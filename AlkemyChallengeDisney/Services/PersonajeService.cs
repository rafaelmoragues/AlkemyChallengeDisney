using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Response;
using AlkemyChallengeDisney.Services.Interfaces;
using AlkemyChallengeDisney.UOfWork;
using AutoMapper;

namespace AlkemyChallengeDisney.Services
{
    public class PersonajeService : IPersonajeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public PersonajeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public IEnumerable<PersonajeResponse> GetPersonajes()
        {
            List<Personaje> listP = _uow.PersonajeRepo.GetAll().ToList();
            List<PersonajeResponse> auxList = new List<PersonajeResponse>();
            auxList = _mapper.Map<List<PersonajeResponse>>(listP);
            return auxList;
        }
        public IEnumerable<PersonajeResponse> GetPersonajesCustom(string campo, object filtro)=>
            campo switch
            {
                "name" => GetListByName(filtro),
                "movie" => GetListByMovie(filtro),
                "age" => GetListByAge(filtro),
                _ => throw new ArgumentException("Invalid string value for campo", nameof(campo)),
            };
            

        
        private List<PersonajeResponse> GetListByName(object filtro)
        {
            string nombre = filtro.ToString();
            List<Personaje> personajeList = _uow.PersonajeRepo.find(x => x.Name == nombre).ToList();
            List<PersonajeResponse> responseList = new List<PersonajeResponse>();
            responseList = _mapper.Map<List<PersonajeResponse>>(personajeList);
            return responseList;
        }
        private List<PersonajeResponse> GetListByAge(object filtro)
        {
            int age = (int)filtro;
            List<Personaje> personajeList = _uow.PersonajeRepo.find(x => x.Age == age).ToList();
            List<PersonajeResponse> responseList = new List<PersonajeResponse>();
            responseList = _mapper.Map<List<PersonajeResponse>>(personajeList);
            return responseList;
        }
        private List<PersonajeResponse> GetListByMovie(object filtro)
        {
            int idMovie = (int)filtro;
            var lista = _uow.PersonajeRepo.GetAll();
            List<Personaje> personajeList = (from p in lista
                             where p.PeliculasList.Where(x => x.Id == idMovie).Count() > 0
                             select p).ToList();
            List<PersonajeResponse> responseList = new List<PersonajeResponse>();
            responseList = _mapper.Map<List<PersonajeResponse>>(personajeList);
            return responseList;
        }
    }
}
