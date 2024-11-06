// pages/Movies.jsx
import { Link } from 'react-router-dom';

const Movies = () => {
  const movies = [
    { id: 1, title: 'Inception' },
    { id: 2, title: 'Interstellar' },
    { id: 3, title: 'The Dark Knight' },
  ];

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-3xl font-bold mb-4">Now Showing</h1>
      <ul className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {movies.map((movie) => (
          <li key={movie.id} className="bg-gray-200 p-4 rounded shadow">
            <h2 className="text-xl font-bold">{movie.title}</h2>
            <Link
              to={`/movies/${movie.id}`}
              className="text-blue-500 hover:underline"
            >
              View Details
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Movies;
