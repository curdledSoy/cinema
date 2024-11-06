using cinema.Data.DTO;
using cinema.Models;

namespace cinema.Services.Interfaces;

public interface ICinemaService
    {
        Task<CinemaDto> GetCinemaByIdAsync(int cinemaId);
        Task<IEnumerable<CinemaDto>> GetAllCinemasAsync();
        Task<IEnumerable<MovieDto>> GetMoviesByCinemaIdAsync(int cinemaId);
        Task<IEnumerable<Screening>> GetScreeningsForMovieInCinemaAsync(int cinemaId, int movieId);
    }