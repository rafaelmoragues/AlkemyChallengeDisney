using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Response;
using AlkemyChallengeDisney.Services.Interfaces;
using AlkemyChallengeDisney.UOfWork;
using AutoMapper;

namespace AlkemyChallengeDisney.Services
{
    public class PeliculaService : IPeliculaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public PeliculaService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public IEnumerable<PeliculaResponse> GetPeliculas()
        {
            List<Pelicula> listP = _uow.PeliculaRepo.GetAll().ToList();
            List<PeliculaResponse> auxList = new List<PeliculaResponse>();
            auxList = MapearPResponse(listP);
            return auxList;
        }

        public IEnumerable<PeliculaResponse> GetPeliculasCustom(string campo, object filtro)
        {
            List<PeliculaResponse> responseList = new List<PeliculaResponse>();
            List<Pelicula> peliculaList = new List<Pelicula>();

            if (campo == "name")
            {
                string nombre = filtro.ToString();
                peliculaList = _uow.PeliculaRepo.find(x => x.Title == nombre).ToList();
            }
            else if (campo == "genre")
            {
                int idGenero = (int)filtro;
                var lista = _uow.PeliculaRepo.GetAll();
                peliculaList = (from p in lista
                                where p.generosList.Where(x => x.Id == idGenero).Count() > 0
                                select p).ToList();
            }
            else if (campo == "asc")
            {
                responseList = (List<PeliculaResponse>)GetPeliculas().OrderBy(x => x.ReleaseDate);
                return responseList;
            }
            else if (campo == "desc")
            {
                responseList = (List<PeliculaResponse>)GetPeliculas().OrderByDescending(x =>x.ReleaseDate);
                return responseList;
            }

            responseList = MapearPResponse(peliculaList);
            return responseList;
        }

        private List<PeliculaResponse> MapearPResponse(List<Pelicula> list)
        {
            List<PeliculaResponse> auxList = new List<PeliculaResponse>();
            PeliculaResponse aux;
            foreach (var p in list)
            {
                aux = _mapper.Map<PeliculaResponse>(p);
                auxList.Add(aux);
            }
            return auxList;
        }
    }
}
