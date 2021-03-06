using AlkemyChallengeDisney.Request;
using AlkemyChallengeDisney.Response;

namespace AlkemyChallengeDisney.Services.Interfaces
{
    public interface IUsuarioService
    {
        UserResponse Registrar(RegisterRequest usuario, string password);
        UserResponse Login(LoginRequest loginUser);
        string GetToken(UserResponse usuario);
    }
}
