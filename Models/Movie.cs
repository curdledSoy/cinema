using System;
using System.Collections.Specialized;
using cinema.Data;
namespace cinema.Models;

public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TimeSpan Duration { get; set; } // Duration of the movie
    public GenreType Genre { get; set; } // Genre of the movie (e.g., Action, Comedy, etc.)
    public DateTime ReleaseDate { get; set; } // Release date of the movie

    public string TrailerUrl {get; set;}

    public string Director {get; set;}

    public ICollection<string>? actors {get; set;}

    // Navigation property for screenings
    public ICollection<Screening> Screenings { get; set; }
}

