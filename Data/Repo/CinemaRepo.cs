using cinema.Models;
using cinema.Data;
using cinema.Data.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace cinema.Data.Repo;

public class CinemaRepo : ICinemaRepo
    {
        private readonly CinemaBookingContext _context;

        public CinemaRepo(CinemaBookingContext context)
        {
            _context = context;
        }

        // Fetch all cinemas
        public async Task<IEnumerable<Cinema>> GetAllAsync()
        {
            return await _context.Cinemas
                .Include(c => c.Theaters)  // Optionally include related entities
                .ToListAsync();
        }

        // Fetch a cinema by ID
        public async Task<Cinema> GetByIdAsync(int cinemaId)
        {
            return await _context.Cinemas
                .Include(c => c.Theaters)  // Optionally include related entities
                .FirstOrDefaultAsync(c => c.CinemaId == cinemaId);
        }
    }