using System;
using cinema.Data;
namespace cinema.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }

    public int ScreeningId { get; set; }
    public int SeatId { get; set; }
    public int UserId { get; set; } // Foreign key for User

    public TicketStatus Status {get; set;}
    public Screening Screening { get; set; }
    public Seat Seat { get; set; }
    public User User { get; set; } // Navigation property for User
}
