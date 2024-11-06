// pages/MovieDetails.jsx
import { useLoaderData, useNavigate, Link, Outlet } from 'react-router-dom';

// Loader function to fetch movie details from an API or database.
const mockMovieData = {
  id: '1',
  title: 'The Grand Adventure',
  genre: 'Adventure, Fantasy',
  duration: '2h 15min',
  rating: 'PG-13',
  director: 'John Doe',
  bannerImage: 'https://via.placeholder.com/800x400?text=Movie+Banner',
  trailer: 'https://www.youtube.com/embed/dQw4w9WgXcQ',
  screenings: [
    { cinema: 'Cinema 1', time: '12:00 PM', seatType: 'Standard' },
    { cinema: 'Cinema 1', time: '3:00 PM', seatType: 'Premium' },
    { cinema: 'Cinema 2', time: '6:00 PM', seatType: 'Standard' },
  ],
  cast: [
    { name: 'Alice Johnson', role: 'Lead Actress' },
    { name: 'Bob Smith', role: 'Supporting Actor' },
  ],
};
export async function loader({ params }) {
 /*  const movieId = params.id;
  const response = await fetch(`https://api.example.com/movies/${movieId}`);
  
  if (!response.ok) throw new Error('Movie not found');
  
  const movie = await response.json();
  return movie; */
  return mockMovieData
}

const MovieDetails = () => {
  const movie = useLoaderData(); // Get movie data from loader
  const navigate = useNavigate(); // For redirection after booking

  const handleBooking = (screening) => {
    // Navigate to the booking modal and pass screening data via state
    navigate('booking', { state: { ...screening, movieTitle: movie.title } });
  };

  return (
    <div className="min-h-screen bg-gray-900 text-white">
      {/* Header Section */}
      <div className="relative h-[400px] bg-cover bg-center"
        style={{ backgroundImage: `url(${movie.bannerImage})` }}>
        <div className="absolute inset-0 bg-black bg-opacity-60 flex items-center justify-center">
          <h1 className="text-4xl font-bold">{movie.title}</h1>
        </div>
      </div>

      {/* Movie Info Section */}
      <div className="container mx-auto px-4 py-8">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          <div>
            <h2 className="text-2xl font-bold mb-4">Movie Information</h2>
            <p><strong>Genre:</strong> {movie.genre}</p>
            <p><strong>Duration:</strong> {movie.duration}</p>
            <p><strong>Rating:</strong> {movie.rating}</p>
            <p><strong>Director:</strong> {movie.director}</p>
          </div>
          <div>
            <h2 className="text-2xl font-bold mb-4">Trailer</h2>
            <iframe
              width="100%"
              height="315"
              src={movie.trailer}
              title={`${movie.title} Trailer`}
              allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
              allowFullScreen
              className="rounded"
            ></iframe>
          </div>
        </div>
      </div>

      {/* Screenings Section with Booking Form */}
      <div className="container mx-auto px-4 py-8">
        <h2 className="text-2xl font-bold mb-4">Available Screenings</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {movie.screenings.map((screening, index) => (
            <div key={index} className="bg-gray-800 p-4 rounded shadow-lg">
              <h3 className="text-xl font-bold">{screening.cinema}</h3>
              <p><strong>Time:</strong> {screening.time}</p>
              <p><strong>Seat Type:</strong> {screening.seatType}</p>
              <Link
                to="booking" state={{ 
                  cinema: screening.cinema,
                  time: screening.time,
                  seatType: screening.seatType,
                  movieTitle: movie.title
                }}
                className="mt-4 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 inline-block"
              >
                Book Now
              </Link>
            </div>
          ))}
        </div>
      </div>

      {/* Cast & Crew Section */}
      <div className="container mx-auto px-4 py-8">
        <h2 className="text-2xl font-bold mb-4">Cast & Crew</h2>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {movie.cast.map((member, index) => (
            <div key={index} className="bg-gray-800 p-4 rounded shadow-lg">
              <h3 className="text-lg font-bold">{member.name}</h3>
              <p className="text-sm text-gray-400">Role: {member.role}</p>
            </div>
          ))}
        </div>
      </div>
      <Outlet/>
    </div>
  );
};

export default MovieDetails;
