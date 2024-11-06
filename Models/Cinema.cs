using System;

namespace cinema.Models;

public class Cinema
{
    public int CinemaId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    
    // Navigation property
    public ICollection<Theater> Theaters { get; set; }
}
