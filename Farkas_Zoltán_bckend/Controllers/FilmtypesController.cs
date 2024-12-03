using Farkas_Zoltán_bckend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farkas_Zoltán_bckend.Controllers
{
    [Route("api/filmtypes")]
    [ApiController]
    public class FilmtypesController : ControllerBase
    {
        private readonly CinemadbContext _context;

        public FilmtypesController(CinemadbContext context)
        {
            _context = context;
        }
        [HttpGet("feladat11")]
        public async Task<ActionResult<FilmType>> Get()
        {
            var filmtype = await _context.FilmTypes.Include(film=> film.Movies).ToListAsync();

            if (filmtype != null)
            {
                return Ok(filmtype);
            }

            Exception e = new Exception();
            return BadRequest(e.Message);
            
        }
    }
}
