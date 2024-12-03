using Farkas_Zoltán_bckend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farkas_Zoltán_bckend.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly CinemadbContext _context;

        public ActorsController(CinemadbContext context)
        {
            _context = context;
        }

        [HttpGet("feladat9")]
        public async Task<ActionResult<Actor>> Get(string name)
        {
            var actor = await _context.Actors.Include(a=> a.Movies).FirstOrDefaultAsync(actor => actor.ActorName == name);

            if (actor != null) 
            {
                return Ok(actor);
            }

            return NotFound();
           
        }

        [HttpGet("feladat12")]
        public async Task<ActionResult<string>> GetCountOfActors()
        {
            var numOfActors = await _context.Actors.CountAsync();

            if(numOfActors != null)
            {
                return Ok($"Szinészek száma {numOfActors}");
            }
           
            return BadRequest("Nem lehet csatlakozni az adatbázishoz.");
        }

        [HttpDelete("feladat16")]
        public async Task<ActionResult> Delete(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(actor => actor.ActorId == id);

            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
                return Ok(new {message = "Sikeres törlés."});
            }

            return NotFound(new { message = "Nincs ilyen színész."});
           
       }
    }
}
