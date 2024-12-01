using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWebAppApi.Model;

namespace MovieWebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MovieController : ControllerBase
    {
        private readonly MovieContext _db;
        public MovieController(MovieContext context)
        {
            _db = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_db.Movies is null)
                return NotFound();

            return await _db.Movies.ToListAsync();

        }
        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if(_db.Movies is null)
                return NotFound();

            var movie = await _db.Movies.FindAsync(id);

            return movie is null ? NotFound() : movie;
        }

        // POST: API/Movies
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new {id = movie.Id}, movie);
        }

        //PUT: API/Movie
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
                return BadRequest();

            _db.Entry(movie).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsMovieExist(id))
                    return NotFound();
                else
                    throw;

            }
            return NoContent();
        }
        private bool IsMovieExist(long id)
        {
            return (_db.Movies?.Any(x => x.Id == id)).GetValueOrDefault();
        }
        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_db.Movies is null)
                return NotFound();

            var movie = await _db.Movies.FindAsync(id);

            if (movie is null)
                return NotFound();

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
