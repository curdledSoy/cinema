// components/Navbar.jsx
import { Link } from 'react-router-dom';

const Navbar = () => (
  <nav className="bg-gray-800 text-white p-4">
    <div className="container mx-auto flex justify-between items-center">
      <Link to="/" className="text-2xl font-bold">
        🎬 CinemaBooking
      </Link>
      <div className="space-x-4">
        <Link to="/" className="hover:underline">
          Home
        </Link>
        <Link to="/movies" className="hover:underline">
          Movies
        </Link>
      </div>
    </div>
  </nav>
);

export default Navbar;
