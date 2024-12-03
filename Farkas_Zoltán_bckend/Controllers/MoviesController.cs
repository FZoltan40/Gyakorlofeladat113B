using Farkas_Zoltán_bckend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farkas_Zoltán_bckend.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly CinemadbContext _context;

        public MoviesController(CinemadbContext context)
        {
            _context = context;
        }

        [HttpGet("feladat10")]
        public async Task<ActionResult<Movie>> Get()
        {
            var movies = await _context.Movies.ToListAsync();

            if (movies != null)
            {
                return Ok(movies);
            }

            return BadRequest();
        }

        [HttpPost("feladat13")]
        public async Task<ActionResult<string>> Post(string uid, Movie movie) 
        {
            var builder = WebApplication.CreateBuilder();
            string id = builder.Configuration.GetValue<string>("Code");

            if (uid == id)
            {
                var mov = new Movie
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ActorId = movie.ActorId,
                    FilmTypeId = movie.FilmTypeId
                };

                if (mov != null)
                {
                    await _context.Movies.AddAsync(mov);
                    await _context.SaveChangesAsync();
                    return StatusCode(201,"Film hozzáadása sikeresen megtörtént.");
                }

                Exception e = new Exception();
                return BadRequest(e.Message);
            }

            return StatusCode(401,"Nincs jogosultsága új film felviteléhez!");
        }
    }
}
