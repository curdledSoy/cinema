// pages/Home.jsx
import { Link } from 'react-router-dom';

const Home = () => {
  const movies = [
    { id: 1, title: 'Inception', image: 'https://via.placeholder.com/300x400' },
    { id: 2, title: 'Interstellar', image: 'https://via.placeholder.com/300x400' },
    { id: 3, title: 'The Dark Knight', image: 'https://via.placeholder.com/300x400' },
  ];

  const upcomingMovies = [
    { id: 4, title: 'Dune: Part Two', image: 'https://via.placeholder.com/300x400' },
    { id: 5, title: 'Avatar 3', image: 'https://via.placeholder.com/300x400' },
  ];

  return (
    <div className="bg-gray-900 text-white min-h-screen">
      {/* Hero Section */}
      <div className="hero h-[60vh] flex items-center justify-center bg-cover bg-center relative"
        style={{ backgroundImage: "url('https://via.placeholder.com/1920x600')" }}>
        <div className="bg-black bg-opacity-50 p-6 rounded">
          <h1 className="text-5xl font-bold mb-4">Welcome to CinemaBooking</h1>
          <p className="text-lg mb-6">
            Book tickets for the latest movies and enjoy an unforgettable experience!
          </p>
          <Link to="/movies" className="px-6 py-3 bg-blue-500 rounded hover:bg-blue-600">
            View Movies
          </Link>
        </div>
      </div>

      {/* Now Showing Section */}
      <section className="container mx-auto px-4 py-8">
        <h2 className="text-3xl font-bold mb-4">Now Showing</h2>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {movies.map((movie) => (
            <div key={movie.id} className="bg-gray-800 p-4 rounded shadow-lg">
              <img src={movie.image} alt={movie.title} className="rounded mb-4" />
              <h3 className="text-xl font-bold">{movie.title}</h3>
              <Link to={`/movies/${movie.id}`} className="text-blue-400 hover:underline mt-2 block">
                View Details
              </Link>
            </div>
          ))}
        </div>
      </section>

      {/* Upcoming Movies Section */}
      <section className="container mx-auto px-4 py-8">
        <h2 className="text-3xl font-bold mb-4">Upcoming Movies</h2>
        <div className="flex space-x-4 overflow-x-auto">
          {upcomingMovies.map((movie) => (
            <div key={movie.id} className="min-w-[200px] bg-gray-800 p-4 rounded shadow-lg">
              <img src={movie.image} alt={movie.title} className="rounded mb-2" />
              <h3 className="text-lg font-bold">{movie.title}</h3>
            </div>
          ))}
        </div>
      </section>

      {/* Newsletter Signup Section */}
      <section className="bg-gray-800 py-8">
        <div className="container mx-auto px-4 text-center">
          <h2 className="text-2xl font-bold mb-4">Stay Updated!</h2>
          <p className="mb-6">Sign up for our newsletter to receive the latest news and offers.</p>
          <form className="flex flex-col md:flex-row justify-center space-y-4 md:space-y-0 md:space-x-4">
            <input
              type="email"
              placeholder="Enter your email"
              className="p-3 w-full md:w-1/3 rounded border border-gray-300 text-black"
            />
            <button
              type="submit"
              className="px-6 py-3 bg-blue-500 text-white rounded hover:bg-blue-600"
            >
              Subscribe
            </button>
          </form>
        </div>
      </section>
    </div>
  );
};

export default Home;
