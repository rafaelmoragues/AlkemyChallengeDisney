using AlkemyChallengeDisney.Request;
using AlkemyChallengeDisney.Response;
using AlkemyChallengeDisney.Services.Interfaces;
using AlkemyChallengeDisney.UOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyChallengeDisney.Controllers
{
    [Route("auth/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUnitOfWork _uow;

        public LoginController(IUsuarioService usuarioService, IUnitOfWork uow)
        {
            _usuarioService = usuarioService;
            _uow = uow;
        }
        [HttpPost]
        public ActionResult Login([FromBody] LoginRequest req)
        {
            var response = _usuarioService.Login(req);
            if (response == null)
            {
                return Unauthorized();
            }
            var token = _usuarioService.GetToken(response);
            return Ok(new
            {
                token = token,
                usuario = response
            });
        }
        [HttpPost("register")]
        public ActionResult RegistrarUsuario([FromBody] RegisterRequest user)
        {
            if (_uow.UsuarioRepo.ExisteUsuario(user.Email.ToLower()))
            {
                return BadRequest("Ya existe un cuenta asociada a ese Email");
            }
            UserResponse res = _usuarioService.Registrar(user, user.Password);
            return Ok(res);
        }
    }
}
