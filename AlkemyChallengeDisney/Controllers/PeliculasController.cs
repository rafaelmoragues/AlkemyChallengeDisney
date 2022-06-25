using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlkemyChallengeDisney.Data;
using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Services.Interfaces;
using AlkemyChallengeDisney.UOfWork;

namespace AlkemyChallengeDisney.Controllers
{
    [Route("movies")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaService _peliculaService;
        private readonly IUnitOfWork _uow;

        public PeliculasController(IPeliculaService peliculaService, IUnitOfWork uow)
        {
            _peliculaService = peliculaService;
            _uow = uow;
        }

        // GET: api/Peliculas
        [HttpGet]
        public ActionResult<IEnumerable<Pelicula>> GetPeliculas([FromQuery] string? name, [FromQuery] string? order, [FromQuery] int? genre)
        {
            return (name, order, genre) switch
            {
                (not null, null, null) => Ok(_peliculaService.GetPeliculasCustom("name", name)),
                (null, "asc", null) => Ok(_peliculaService.GetPeliculasCustom("asc", order)),
                (null, "desc", null) => Ok(_peliculaService.GetPeliculasCustom("desc", order)),
                (null, null, not null) => Ok(_peliculaService.GetPeliculasCustom("genre", genre)),
                _ => Ok(_peliculaService.GetPeliculas()),
            };
        }

        // GET: api/Peliculas/5
        [HttpGet("{id}")]
        public ActionResult<Pelicula> GetPelicula(int id)
        {
          if (_uow.PeliculaRepo == null)
          {
              return NotFound();
          }
            var pelicula = _uow.PeliculaRepo.GetById(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula;
        }

        // PUT: api/Peliculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPelicula(int id, Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return BadRequest();
            }
            _uow.PeliculaRepo.Update(pelicula);
            _uow.Save();            

            return NoContent();
        }

        // POST: api/Peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Pelicula> PostPelicula(Pelicula pelicula)
        {
          if (_uow.PeliculaRepo == null)
          {
                return BadRequest();
          }
            _uow.PeliculaRepo.Insert(pelicula);
            _uow.Save();

            return Ok();
        }

        // DELETE: api/Peliculas/5
        [HttpDelete("{id}")]
        public IActionResult DeletePelicula(int id)
        {
            if (_uow.PeliculaRepo == null)
            {
                return NotFound();
            }
            var pelicula = _uow.PeliculaRepo.GetById(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _uow.PeliculaRepo.Delete(id);
            _uow.Save();

            return NoContent();
        }

    }
}
