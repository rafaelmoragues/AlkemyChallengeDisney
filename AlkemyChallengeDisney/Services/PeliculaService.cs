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
            auxList = _mapper.Map<List<PeliculaResponse>>(listP);
            return auxList;
        }

        public IEnumerable<PeliculaResponse> GetPeliculasCustom(string campo, object filtro) =>

            campo switch
            {
                "name" => GetListByName(filtro),
                "genre" => GetListByGenre(filtro),
                "asc" => GetListByAsc(filtro),
                "desc" => GetListByDesc(filtro),
                _ => throw new ArgumentException("Invalid string value for campo", nameof(campo)),

            };
        private List<PeliculaResponse> GetListByName(object filtro)
        {
            string nombre = filtro.ToString();
            List<Pelicula> peliculaList = _uow.PeliculaRepo.find(x => x.Title == nombre).ToList();
            List<PeliculaResponse> responseList = _mapper.Map<List<PeliculaResponse>>(peliculaList);
            return responseList;
        }
        private List<PeliculaResponse> GetListByGenre(object filtro)
        {
            int idGenero = (int)filtro;
            var lista = _uow.PeliculaRepo.GetAll();
            List<Pelicula> peliculaList = (from p in lista
                                           where p.generosList.Where(x => x.Id == idGenero).Count() > 0
                                           select p).ToList();
            List<PeliculaResponse> responseList = _mapper.Map<List<PeliculaResponse>>(peliculaList);
            return responseList;
        }
        private List<PeliculaResponse> GetListByAsc(object filtro)
        {
            List<PeliculaResponse> responseList = (List<PeliculaResponse>)GetPeliculas().OrderBy(x => x.ReleaseDate);
            return responseList;
        }
        private List<PeliculaResponse> GetListByDesc(object filtro)
        {
            List<PeliculaResponse> responseList = (List<PeliculaResponse>)GetPeliculas().OrderByDescending(x => x.ReleaseDate);
            return responseList;
        }
    }
}
