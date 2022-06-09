using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Request;
using AlkemyChallengeDisney.Response;
using AlkemyChallengeDisney.UOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AlkemyChallengeDisney.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _uOW;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UsuarioService(IUnitOfWork uow, IConfiguration configuration, IMapper mapper)
        {
            _uOW = uow;
            _configuration = configuration;
            _mapper = mapper;
        }

        public UserResponse Login(LoginRequest userLogin)
        {
            if (_uOW.UsuarioRepo.ExisteUsuario(userLogin.Email))
            {
                UserResponse response = new UserResponse();
                //traigo el usuario, por el email
                Usuario user = _uOW.UsuarioRepo.GetByEmail(userLogin.Email);
                //verifico si el password ingresado es el mismo del usuario en la DB
                if (!VerificarPassword(userLogin.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }
                //aca mappeo de un Usuario a un UserResponse para no devolver cosas innecesarias
                response = _mapper.Map<UserResponse>(user);
                //Devuelvo la respuesta si esta todo bien
                return response;
            }
            return null;
        }
        private bool VerificarPassword(string pass, byte[] pHash, byte[] pSalt)
        {
            //hago una encriptacion con la key (psalt)
            var hMac = new HMACSHA512(pSalt);
            var hash = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
            //comparo el pass de la DB con el que acabo de encriptar
            for (var i = 0; i < hash.Length; i++)
            {
                if (hash[i] != pHash[i]) return false;
            }

            return true;
        }
        public UserResponse Registrar(RegisterRequest user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            CrearPassHash(password, out passwordHash, out passwordSalt);
            Usuario usuario = _mapper.Map<Usuario>(user);
            usuario.FechaAlta = DateTime.Now;
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            usuario.Role = Role.User;
            _uOW.UsuarioRepo.Insert(usuario);
            _uOW.Save();
            UserResponse response = _mapper.Map<UserResponse>(usuario);
            return response;
        }
        private void CrearPassHash(string pass, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //creo una encriptacion
            var hMac = new HMACSHA512();
            //le asigno la llave de la encriptacion al passwordSalt
            passwordSalt = hMac.Key;
            //Encripto el pass y lo guardo en passwordHash
            passwordHash = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));


        }

        public string GetToken(UserResponse usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(ClaimTypes.Role, usuario.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(120),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials

            };
            var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
