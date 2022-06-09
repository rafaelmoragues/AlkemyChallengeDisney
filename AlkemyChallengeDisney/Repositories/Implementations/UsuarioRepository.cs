using AlkemyChallengeDisney.Data;
using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Repositories.Interfaces;

namespace AlkemyChallengeDisney.Repositories.Implementations
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
        }
        public Usuario GetByEmail(string email)
        {
            return _db.Usuarios.FirstOrDefault(a => a.Email == email);
        }
        public bool ExisteUsuario(string email)
        {
            return _db.Usuarios.Any(a => a.Email == email);
        }
    }
}
