import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import RootLayout from './components/Root';
import Home from './routes';
import Movies from './routes/Movies';
import MovieDetails, { loader as movieDetailsLoader } from './routes/MovieDetails';
import Booking, { action as bookingAction} from './routes/Booking';

// Define the routes with the root layout
const router = createBrowserRouter([
  {
    path: '/', // Root route with layout
    element: <RootLayout />,
    children: [
      { path: '/', element: <Home /> },
      { path: 'movies', element: <Movies /> },
      { 
        path: 'movies/:id', 
        element: <MovieDetails />, 
        loader: movieDetailsLoader, 
        children: [{ 
          path: 'booking', 
          element: <Booking />, 
          action: bookingAction 
        }]
      },
    ],
  },
]);

// Render the RouterProvider
function App() {
  return <RouterProvider router={router} />;
}

export default App
