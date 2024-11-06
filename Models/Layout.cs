namespace cinema.Models;

public class Layout
{
    public int LayoutId { get; set; }
    public string LayoutName { get; set; } // e.g., "Standard", "VIP"
    public int Rows { get; set; } // Number of rows in the grid
    public int Columns { get; set; } // Number of columns in the grid
    public ICollection<Seat> Seats { get; set; } // One-to-many relationship with Seats

    public Theater? Theater {get; set;}

    public int TheaterId {get; set;}

    
}
