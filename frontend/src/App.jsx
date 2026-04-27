import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import Home from './pages/Home';
import About from './pages/About';
import Contact from './pages/Contact';
import AdminDashboard from './pages/AdminDashboard';

function App() {
  return (
    <Router>
      <Navbar />
      <div style={{ minHeight: '80vh' }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/blog" element={<div style={{ textAlign: 'center', marginTop: '50px' }}><h1>Blog</h1><p>Coming soon...</p></div>} />
          <Route path="/admin" element={<AdminDashboard />} />
        </Routes>
      </div>
      <footer style={styles.footer}>
        <p>&copy; 2026 Ticket Master. Built with React & .NET</p>
      </footer>
    </Router>
  );
}

const styles = {
  footer: { textAlign: 'center', padding: '20px', backgroundColor: '#2c3e50', color: 'white', marginTop: '40px' }
};

export default App;