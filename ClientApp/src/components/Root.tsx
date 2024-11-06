
import { Outlet } from 'react-router-dom';
import Navbar from './NavBar';
import Footer from './Footer';

const Root = () => {
  return (
    <div className="flex flex-col min-h-screen bg-gray-100">
      <Navbar />
      <main className="flex-grow">
        <Outlet /> {/* Child routes will render here */}
      </main>
      <Footer />
    </div>
  );
};

export default Root;
