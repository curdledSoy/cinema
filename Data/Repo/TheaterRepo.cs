using cinema.Data.Repo.Interfaces;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Data.Repositories
{
    public class TheaterRepo : ITheaterRepo
    {
        private readonly CinemaBookingContext _context;

        public TheaterRepo(CinemaBookingContext context)
        {
            _context = context;
        }

        // Get all theaters for a specific cinema
        public async Task<IEnumerable<Theater>> GetTheatersByCinemaIdAsync(int cinemaId)
        {
            return await _context.Theaters
                .Where(t => t.CinemaId == cinemaId)
                .Include(t => t.Screenings)  // Optionally include related screenings
                .Include(t => t.Seats)       // Optionally include related seats
                .ToListAsync();
        }

        // Get a specific theater by its ID
        public async Task<Theater> GetByIdAsync(int theaterId)
        {
            return await _context.Theaters
                .Include(t => t.Screenings)  // Optionally include related screenings
                .Include(t => t.Seats)       // Optionally include related seats
                .FirstOrDefaultAsync(t => t.TheaterId == theaterId);
        }
    }
}
