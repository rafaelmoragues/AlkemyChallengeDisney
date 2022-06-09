using AlkemyChallengeDisney.Data;
using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Repositories.Interfaces;
using AlkemyChallengeDisney.Response;
using Microsoft.EntityFrameworkCore;

namespace AlkemyChallengeDisney.Repositories.Implementations
{
    public class PersonajeRepository : GenericRepository<Personaje>, IPersonajeRepository
    {
        public PersonajeRepository(ApplicationDbContext db) : base(db)
        {
        }

        public Personaje GetPersonajeFull(int id)
        {
            return _db.Personajes.Where(x => x.Id == id).Include(y => y.PeliculasList).FirstOrDefault();
        }        
    }
}
