using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlkemyChallengeDisney.Data;
using AlkemyChallengeDisney.Models;
using AlkemyChallengeDisney.Services;
using AlkemyChallengeDisney.UOfWork;
using AlkemyChallengeDisney.Services.Interfaces;

namespace AlkemyChallengeDisney.Controllers
{
    [Route("characters")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly IPersonajeService _personajeService;
        private readonly IUnitOfWork _uow;

        public PersonajesController(IPersonajeService personajeService, IUnitOfWork uow)
        {
            _personajeService = personajeService;
            _uow = uow;
        }

        // GET: api/Personajes
        //[HttpGet]
        //public ActionResult<IEnumerable<Personaje>> GetPersonajes()
        //{
        //  if (_uow.PersonajeRepo == null)
        //  {
        //      return NotFound();
        //  }
        //    return Ok(_personajeService.GetPersonajes());
        //}
        [HttpGet("")]
        public ActionResult<IEnumerable<Personaje>> GetPersonajesCustom([FromQuery] string? name, [FromQuery] int? age, [FromQuery] int? movies)
        {
            return (name, age, movies) switch
            {
                (not null, null, null) => Ok(_personajeService.GetPersonajesCustom("name", name)),
                (null, not null, null) => Ok(_personajeService.GetPersonajesCustom("age", age)),
                (null, null, not null) => Ok(_personajeService.GetPersonajesCustom("movies", movies)),
                _ => Ok(_personajeService.GetPersonajes()),
            };
            
        }

        // GET: api/Personajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>> GetPersonaje(int id)
        {
          if (_uow.PersonajeRepo == null)
          {
              return NotFound();
          }
            var personaje = _uow.PersonajeRepo.GetPersonajeFull(id);

            if (personaje == null)
            {
                return NotFound();
            }

            return personaje;
        }

        
        // PUT: api/Personajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPersonaje(int id, Personaje personaje)
        {
            if (id != personaje.Id)
            {
                return BadRequest();
            }

            _uow.PersonajeRepo.Update(personaje);

            try
            {
                _uow.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Personajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Personaje> PostPersonaje(Personaje personaje)
        {
          if (_uow.PersonajeRepo == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Personajes'  is null.");
          }
            _uow.PersonajeRepo.Insert(personaje);
            _uow.Save();

            return CreatedAtAction("GetPersonaje", new { id = personaje.Id }, personaje);
        }

        // DELETE: api/Personajes/5
        [HttpDelete("{id}")]
        public IActionResult DeletePersonaje(int id)
        {
            if (_uow.PersonajeRepo == null)
            {
                return NotFound();
            }
            var personaje = _uow.PersonajeRepo.GetById(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _uow.PersonajeRepo.Delete(id);
            _uow.Save();

            return NoContent();
        }

        private bool PersonajeExists(int id)
        {
            var personaje = _uow.PersonajeRepo.GetById(id);
            if (personaje == null)
            {
                return false;
            }
            return true;
        }
    }
}
