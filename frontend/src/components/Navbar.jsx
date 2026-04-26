import { Link } from 'react-router-dom';

const Navbar = () => (
  <nav style={styles.nav}>
    <div style={styles.logo}>TICKET<span style={{color: '#818cf8'}}>HUB</span></div>
    <div style={styles.links}>
      <Link to="/" style={styles.link}>Events</Link>
      <Link to="/blog" style={styles.link}>News</Link>
      <Link to="/about" style={styles.link}>About</Link>
      <Link to="/contact" style={styles.contactBtn}>Sign In</Link>
    </div>
  </nav>
);

const styles = {
  nav: { 
    display: 'flex', justifyContent: 'space-between', alignItems: 'center',
    padding: '20px 8%', background: 'rgba(15, 23, 42, 0.8)', 
    backdropFilter: 'blur(10px)', borderBottom: '1px solid rgba(255,255,255,0.1)',
    position: 'sticky', top: 0, zIndex: 1000 
  },
  logo: { fontSize: '24px', fontWeight: '800', letterSpacing: '1px' },
  links: { display: 'flex', alignItems: 'center', gap: '35px' },
  link: { color: '#cbd5e1', textDecoration: 'none', fontWeight: '500', fontSize: '15px' },
  contactBtn: { 
    background: 'linear-gradient(45deg, #6366f1, #a855f7)', color: '#fff', 
    padding: '10px 25px', borderRadius: '50px', textDecoration: 'none', 
    fontWeight: '600', boxShadow: '0 4px 15px rgba(99, 102, 241, 0.4)' 
  }
};

export default Navbar;