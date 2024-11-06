 
 namespace cinema.Data.DTO;
 public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }