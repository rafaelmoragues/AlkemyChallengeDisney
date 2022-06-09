using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Response;

namespace AlkemyChallengeDisney.Repositories.Interfaces
{
    public interface IPersonajeRepository : IGenericRepository<Personaje>
    {
        Personaje GetPersonajeFull(int id);
    }
}
