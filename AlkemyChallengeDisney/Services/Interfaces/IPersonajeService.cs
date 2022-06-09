using AlkemyChallengeDisney.Response;

namespace AlkemyChallengeDisney.Services.Interfaces
{
    public interface IPersonajeService
    {
        IEnumerable<PersonajeResponse> GetPersonajes();
        IEnumerable<PersonajeResponse> GetPersonajesCustom(string campo,object filtro);
    }
}
