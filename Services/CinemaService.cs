using cinema.Data.Repo.Interfaces;
using cinema.Services.Interfaces;
using cinema.Data.DTO;
using AutoMapper;
using System.Linq;
namespace cinema.Services;


 public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepo _cinemaRepository;
        private readonly ITheaterRepo _theaterRepository;
        private readonly IScreeningRepo _screeningRepository;
        private readonly IMovieRepo _movieRepository;
        private readonly IMapper _mapper;

        public CinemaService(
            ICinemaRepo cinemaRepository,
            ITheaterRepo theaterRepository,
            IScreeningRepo screeningRepository,
            IMovieRepo movieRepository,
            IMapper mapper)
        {
            _cinemaRepository = cinemaRepository;
            _theaterRepository = theaterRepository;
            _screeningRepository = screeningRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        // Retrieve all cinemas
        public async Task<IEnumerable<CinemaDto>> GetAllCinemasAsync()
        {
            var cinemas = await _cinemaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CinemaDto>>(cinemas);
        }

        // Retrieve a specific cinema by ID
        public async Task<CinemaDto> GetCinemaByIdAsync(int cinemaId)
        {
            var cinema = await _cinemaRepository.GetByIdAsync(cinemaId);
            if (cinema == null) throw new KeyNotFoundException("Cinema not found");
            return _mapper.Map<CinemaDto>(cinema);
        }

        // Retrieve theaters associated with a specific cinema ID
        public async Task<IEnumerable<TheaterDto>> GetTheatersByCinemaIdAsync(int cinemaId)
        {
            var theaters = await _theaterRepository.GetTheatersByCinemaIdAsync(cinemaId);
            return _mapper.Map<IEnumerable<TheaterDto>>(theaters);
        }

        // Retrieve movies with screenings at a specific cinema
        public async Task<IEnumerable<MovieDto>> GetMoviesWithScreeningsAsync(int cinemaId)
        {
            // Get all screenings at the specified cinema
            var screenings = await _screeningRepository.GetScreeningsByCinemaIdAsync(cinemaId);

            // Extract distinct movie IDs from the screenings
            var movieIds = screenings.Select(s => s.MovieId).Distinct();

            // Retrieve movies by the list of IDs
            var movies = await _movieRepository.GetMoviesByIdsAsync(movieIds);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }
    }