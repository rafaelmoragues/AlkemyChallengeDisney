using AlkemyChallengeDisney.Repositories.Interfaces;

namespace AlkemyChallengeDisney.UOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository UsuarioRepo { get; }
        IGeneroRepository GeneroRepo { get; }
        IPeliculaRepository PeliculaRepo { get; }
        IPersonajeRepository PersonajeRepo { get; }
        void Save();
    }
}
