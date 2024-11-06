using cinema.Models;

namespace cinema.Data.Repo.Interfaces;

public interface ICinemaRepo
    {
        Task<IEnumerable<Cinema>> GetAllAsync();
        Task<Cinema> GetByIdAsync(int cinemaId);
    }