using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cinema.Data;
using cinema.Models;

namespace cinema.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly CinemaBookingContext _context;

        public BookingController(CinemaBookingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new booking for a specific screening.
        /// </summary>
        /// <param name="bookingRequest">Booking details including screening, seats, and user information.</param>
        /// <returns>The created booking with ticket details.</returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Ticket>>> MakeBooking(BookingRequest bookingRequest)
        {
            // Validate if the screening exists
            var screening = await _context.Screenings
                                          .Include(s => s.Theater)
                                          .FirstOrDefaultAsync(s => s.ScreeningId == bookingRequest.ScreeningId);
            if (screening == null)
                return NotFound("Screening not found.");

            // Validate seats are available for booking
            var unavailableSeats = await _context.Tickets
                .Where(t => t.ScreeningId == bookingRequest.ScreeningId && 
                            bookingRequest.SeatIds.Contains(t.SeatId))
                .Select(t => t.SeatId)
                .ToListAsync();

            if (unavailableSeats.Any())
                return BadRequest($"Seats {string.Join(", ", unavailableSeats)} are already booked.");

            // Create tickets for each seat
            var tickets = bookingRequest.SeatIds.Select(seatId => new Ticket
            {
                ScreeningId = bookingRequest.ScreeningId,
                SeatId = seatId,
                UserId = (int)bookingRequest.UserId,
                PurchaseDate = DateTime.Now
            }).ToList();

            _context.Tickets.AddRange(tickets);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingById), new { id = tickets.First().TicketId }, tickets);
        }

        /// <summary>
        /// Get the details of a booking by Ticket ID.
        /// </summary>
        /// <param name="id">Ticket ID.</param>
        /// <returns>The booking details including screening and seat information.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetBookingById(int id)
        {
            var ticket = await _context.Tickets
                                       .Include(t => t.Screening)
                                       .ThenInclude(s => s.Movie)
                                       .Include(t => t.Seat)
                                       .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
                return NotFound("Booking not found.");

            return Ok(ticket);
        }

        /// <summary>
        /// Cancel a booking by Ticket ID.
        /// </summary>
        /// <param name="id">Ticket ID.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound("Booking not found.");

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    /// <summary>
    /// Request model for making a booking.
    /// </summary>
    public class BookingRequest
    {
        public int ScreeningId { get; set; }
        public List<int> SeatIds { get; set; }
        public int? UserId { get; set; } // Optional if anonymous booking
    }
}
