using System;

namespace cinema.Models;

public class Screening
{
    public int ScreeningId { get; set; }
    public DateTime StartTime { get; set; }
    public int TheaterId { get; set; }
    public int MovieId { get; set; } // Foreign key for Movie

    public Theater Theater { get; set; }
    public Movie Movie { get; set; } // Navigation property for Movie
    public ICollection<Ticket> Tickets { get; set; }
}

