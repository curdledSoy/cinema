using cinema.Models;

namespace cinema.Data.Repo.Interfaces;

public interface IMovieRepo
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<IEnumerable<Movie>> GetMoviesByIdsAsync(IEnumerable<int> ids);
    Task<Movie> GetMovieByIdAsync(int movieId);
    Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
    Task<IEnumerable<Movie>> GetMoviesByTitleAsync(string title);
}

