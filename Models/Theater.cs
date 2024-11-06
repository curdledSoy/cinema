using System;

namespace cinema.Models;
public class Theater
{
    public int TheaterId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public Layout Layout { get; set; }
    public int CinemaId { get; set; }
    
    public Cinema Cinema { get; set; }
    public ICollection<Seat> Seats { get; set; }
    public ICollection<Screening> Screenings { get; set; }
}
