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
            auxList = MapearPResponse(listP);
            return auxList;
        }
        public IEnumerable<PersonajeResponse> GetPersonajesCustom(string campo, object filtro)
        {
            List<PersonajeResponse> responseList = new List<PersonajeResponse>();
            List<Personaje> personajeList = new List<Personaje>();

            if (campo == "name")
            {
                string nombre = filtro.ToString();
                personajeList = _uow.PersonajeRepo.find(x => x.Name == nombre).ToList();
            }
            else if(campo == "movie")
            {
                int idMovie = (int)filtro;
                var lista = _uow.PersonajeRepo.GetAll();
                personajeList = (from p in lista
                         where p.PeliculasList.Where(x=>x.Id == idMovie).Count() > 0
                         select p).ToList();
            }
            else if (campo == "age")
            {
                int age = (int)filtro;
                personajeList = _uow.PersonajeRepo.find(x => x.Age == age).ToList();
            }

            responseList = MapearPResponse(personajeList);
            return responseList;
        }
        private List<PersonajeResponse> MapearPResponse(List<Personaje> list)
        {
            List<PersonajeResponse> auxList = new List<PersonajeResponse>();
            PersonajeResponse aux;
            foreach (var p in list)
            {
                aux = _mapper.Map<PersonajeResponse>(p);
                auxList.Add(aux);
            }
            return auxList;
        }
    }
}
