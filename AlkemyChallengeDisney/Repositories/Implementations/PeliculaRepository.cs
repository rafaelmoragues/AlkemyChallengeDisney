using AlkemyChallengeDisney.Data;
using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Repositories.Interfaces;

namespace AlkemyChallengeDisney.Repositories.Implementations
{
    public class PeliculaRepository : GenericRepository<Pelicula>, IPeliculaRepository
    {
        public PeliculaRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
