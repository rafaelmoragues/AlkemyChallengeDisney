using AlkemyChallengeDisney.Data;
using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Repositories.Interfaces;

namespace AlkemyChallengeDisney.Repositories.Implementations
{
    public class GeneroRepository : GenericRepository<Genero>, IGeneroRepository
    {
        public GeneroRepository(ApplicationDbContext db) : base(db)
        {

        }
    }
}
