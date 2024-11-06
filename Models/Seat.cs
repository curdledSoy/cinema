using System;
using cinema.Data;

namespace cinema.Models;


public class Seat
{
    public int SeatId { get; set; }
    public int Row { get; set; } // Row position in the grid
    public int Column { get; set; } // Column position in the grid
    public SeatType SeatType { get; set; } // e.g., "Regular", "VIP"
    public int LayoutId { get; set; } // Foreign key to Layout
    public Layout Layout { get; set; }
    public int TheaterId { get; set; }
    
    public Theater Theater { get; set; }

    // Navigation property to relate to Ticket
    public ICollection<Ticket> Tickets { get; set; }
}


