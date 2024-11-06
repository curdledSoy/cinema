using cinema.Models;
namespace cinema.Data.Repo.Interfaces;

    public interface IScreeningRepo
    {
        Task<IEnumerable<Screening>> GetScreeningsByMovieIdAsync(int movieId);
        Task<IEnumerable<Screening>> GetScreeningsByTheaterIdAsync(int theaterId);
        Task<IEnumerable<Screening>> GetScreeningsByCinemaIdAsync(int cinemaId);
        Task<Screening> GetScreeningByIdAsync(int screeningId);
        Task<IEnumerable<Screening>> GetAllScreeningsAsync();
    }
