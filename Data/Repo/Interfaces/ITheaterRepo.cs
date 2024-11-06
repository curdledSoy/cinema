using cinema.Models;

namespace cinema.Data.Repo.Interfaces;

public interface ITheaterRepo
{
    Task<IEnumerable<Theater>> GetTheatersByCinemaIdAsync(int cinemaId);
    Task<Theater> GetByIdAsync(int theaterId);
}

