using cinema.Data;
using cinema.Models;
using cinema.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cinema.Data.Repo;

    public class MovieRepo : IMovieRepo
    {
        private readonly CinemaBookingContext _context;

        public MovieRepo(CinemaBookingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(m => m.Screenings) // Include screenings for the movie
                .ThenInclude(s => s.Theater) // Include theater information
                .ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await _context.Movies
                .Include(m => m.Screenings)
                .ThenInclude(s => s.Theater)
                .FirstOrDefaultAsync(m => m.MovieId == movieId);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
        {
            return await _context.Movies
                .Where(m => m.Genre.ToString().Equals(genre, System.StringComparison.OrdinalIgnoreCase))
                .Include(m => m.Screenings)
                .ThenInclude(s => s.Theater)
                .ToListAsync();
        }

    public Task<IEnumerable<Movie>> GetMoviesByIdsAsync(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Movie>> GetMoviesByTitleAsync(string title)
        {
            return await _context.Movies
                .Where(m => m.Title.Contains(title))
                .Include(m => m.Screenings)
                .ThenInclude(s => s.Theater)
                .ToListAsync();
        }
    }
