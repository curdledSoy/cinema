using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cinema.Data;
using cinema.Models;

namespace cinema.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly CinemaBookingContext _context;

        public MovieController(CinemaBookingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all movies with optional pagination and filtering.
        /// </summary>
        /// <param name="pageNumber">Page number (default is 1).</param>
        /// <param name="pageSize">Page size (default is 10).</param>
        /// <param name="genre">Optional genre filter.</param>
        /// <param name="search">Optional search by movie title.</param>
        /// <param name="start">Optional start date for filtering by screening times.</param>
        /// <param name="end">Optional end date for filtering by screening times.</param>
        /// <returns>List of movies.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies(
            int pageNumber = 1, 
            int pageSize = 10, 
            string genre = null, 
            string search = null, 
            DateTime? start = null, 
            DateTime? end = null)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Page number and page size must be greater than 0.");

            var query = _context.Movies.Include(m => m.Screenings).AsQueryable();

            // Filter by genre if provided
            if (!string.IsNullOrEmpty(genre) && Enum.TryParse(typeof(GenreType), genre, true, out var genreEnum))
                query = query.Where(m => m.Genre == (GenreType)genreEnum);

            // Filter by search term if provided
            if (!string.IsNullOrEmpty(search))
                query = query.Where(m => m.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

            // Filter by screening dates if start or end is provided
            if (start.HasValue || end.HasValue)
            {
                DateTime startDate = start ?? DateTime.MinValue;
                DateTime endDate = end ?? DateTime.MaxValue;

                query = query.Where(m => m.Screenings.Any(s => s.StartTime >= startDate && s.StartTime <= endDate));
            }

            var movies = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(movies);
        }
        /// <summary>
        /// Get a specific movie by ID.
        /// </summary>
        /// <param name="id">Movie ID.</param>
        /// <returns>The movie details.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound($"Movie with ID {id} not found.");

            return Ok(movie);
        }

        /// <summary>
        /// Update a movie's details.
        /// </summary>
        /// <param name="id">Movie ID to update.</param>
        /// <param name="movie">Updated movie object.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
                return BadRequest("Movie ID in the URL does not match the movie object.");

            if (!MovieExists(id))
                return NotFound($"Movie with ID {id} not found.");

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                    return NotFound($"Movie with ID {id} not found.");
                else
                    throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Create a new movie.
        /// </summary>
        /// <param name="movie">New movie object.</param>
        /// <returns>The created movie.</returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            if (string.IsNullOrEmpty(movie.Title) || movie.Duration <= TimeSpan.Zero)
                return BadRequest("Invalid movie data. Title and duration are required.");

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.MovieId }, movie);
        }

        /// <summary>
        /// Delete a movie by ID.
        /// </summary>
        /// <param name="id">Movie ID to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound($"Movie with ID {id} not found.");

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if a movie exists by ID.
        /// </summary>
        /// <param name="id">Movie ID.</param>
        /// <returns>True if the movie exists, false otherwise.</returns>
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
