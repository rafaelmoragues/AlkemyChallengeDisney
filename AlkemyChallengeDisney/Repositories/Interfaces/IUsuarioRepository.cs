using AlkemyChallengeDisney.Models;

namespace AlkemyChallengeDisney.Repositories.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Usuario GetByEmail(string email);
        bool ExisteUsuario(string email);
    }
}
