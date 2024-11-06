using cinema.Data;
using cinema.Models;
using cinema.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace cinema.Data.Repo;

public class ScreeningRepo : IScreeningRepo
{
    private readonly CinemaBookingContext _context;

    public ScreeningRepo(CinemaBookingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Screening>> GetScreeningsByMovieIdAsync(int movieId)
    {
        return await _context.Screenings
            .Where(s => s.MovieId == movieId)
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .ThenInclude(t => t.Seats)
            .ToListAsync();
    }

    public async Task<IEnumerable<Screening>> GetScreeningsByTheaterIdAsync(int theaterId)
    {
        return await _context.Screenings
            .Where(s => s.TheaterId == theaterId)
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .ThenInclude(t => t.Seats)
            .ToListAsync();
    }

    public async Task<Screening> GetScreeningByIdAsync(int screeningId)
    {
        return await _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .ThenInclude(t => t.Seats)
            .FirstOrDefaultAsync(s => s.ScreeningId == screeningId);
    }

    public async Task<IEnumerable<Screening>> GetAllScreeningsAsync()
    {
        return await _context.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .ThenInclude(t => t.Seats)
            .ToListAsync();
    }

    public async Task<IEnumerable<Screening>> GetScreeningsByCinemaIdAsync(int cinemaId)
    {
        return await _context.Screenings
            .Where(s => s.Theater.CinemaId == cinemaId)
            .Include(s => s.Movie)
            .Include(s => s.Theater)
            .ThenInclude(t => t.Seats)
            .ToListAsync();
    }
}
