using AlkemyChallengeDisney.Data;
using AlkemyChallengeDisney.Repositories.Implementations;
using AlkemyChallengeDisney.Repositories.Interfaces;

namespace AlkemyChallengeDisney.UOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUsuarioRepository UsuarioRepo { get; private set; }

        public IGeneroRepository GeneroRepo { get; private set; }

        public IPeliculaRepository PeliculaRepo { get; private set; }

        public IPersonajeRepository PersonajeRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            UsuarioRepo = new UsuarioRepository(context);
            GeneroRepo = new GeneroRepository(context);
            PeliculaRepo = new PeliculaRepository(context);
            PersonajeRepo = new PersonajeRepository(context);
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

