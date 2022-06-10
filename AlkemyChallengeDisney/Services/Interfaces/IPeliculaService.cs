using AlkemyChallengeDisney.Response;

namespace AlkemyChallengeDisney.Services.Interfaces
{
    public interface IPeliculaService
    {
        IEnumerable<PeliculaResponse> GetPeliculas();
        IEnumerable<PeliculaResponse> GetPeliculasCustom(string campo, object filtro);
    }
}
