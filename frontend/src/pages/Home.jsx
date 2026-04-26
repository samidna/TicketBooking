import { useEffect, useState } from 'react';
import api from '../api';

const Home = () => {
  const [events, setEvents] = useState([]);

  useEffect(() => {
    api.get('/Events').then(res => setEvents(res.data));
  }, []);

  return (
    <div style={styles.container}>
      {/* Hero Section */}
      <div style={styles.hero}>
        <h1 style={styles.heroTitle}>Don't Miss Your <br/> <span style={styles.gradientText}>Next Big Event</span></h1>
        <p style={styles.heroSub}>Secure your spot at the most anticipated concerts and shows worldwide.</p>
      </div>

      {/* Events Grid */}
      <div style={styles.grid}>
        {events.map(event => (
          <div key={event.id} style={styles.card}>
            <div style={styles.imageBox}>
               <span style={styles.dateTag}>LIVE</span>
            </div>
            <div style={styles.cardContent}>
              <h3 style={styles.eventTitle}>{event.name}</h3>
              <p style={styles.eventInfo}>📍 Baku Olympic Stadium</p>
              <div style={styles.divider}></div>
              <div style={styles.footer}>
                <div>
                  <p style={styles.priceLabel}>Price from</p>
                  <span style={styles.price}>${event.price}</span>
                </div>
                <button style={styles.buyBtn}>Buy Now</button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

const styles = {
  container: { padding: '60px 8%', minHeight: '100vh' },
  hero: { textAlign: 'left', marginBottom: '80px' },
  heroTitle: { fontSize: '64px', fontWeight: '800', lineHeight: '1.1', marginBottom: '20px' },
  gradientText: { background: 'linear-gradient(90deg, #6366f1, #a855f7)', WebkitBackgroundClip: 'text', WebkitTextFillColor: 'transparent' },
  heroSub: { fontSize: '18px', color: '#94a3b8', maxWidth: '600px' },
  
  grid: { display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(320px, 1fr))', gap: '30px' },
  card: { 
    background: '#1e293b', borderRadius: '24px', overflow: 'hidden',
    border: '1px solid rgba(255,255,255,0.05)', transition: '0.3s'
  },
  imageBox: { 
    height: '200px', background: 'linear-gradient(135deg, #334155 0%, #0f172a 100%)',
    position: 'relative', display: 'flex', alignItems: 'center', justifyContent: 'center'
  },
  dateTag: { 
    position: 'absolute', top: '20px', left: '20px', background: '#ef4444', 
    padding: '5px 15px', borderRadius: '20px', fontSize: '12px', fontWeight: 'bold' 
  },
  cardContent: { padding: '25px' },
  eventTitle: { fontSize: '22px', fontWeight: '600', marginBottom: '8px' },
  eventInfo: { color: '#94a3b8', fontSize: '14px', marginBottom: '20px' },
  divider: { height: '1px', background: 'rgba(255,255,255,0.1)', marginBottom: '20px' },
  footer: { display: 'flex', justifyContent: 'space-between', alignItems: 'center' },
  priceLabel: { fontSize: '12px', color: '#64748b', margin: 0 },
  price: { fontSize: '24px', fontWeight: '700', color: '#fff' },
  buyBtn: { 
    padding: '12px 25px', border: 'none', borderRadius: '15px', 
    background: '#fff', color: '#0f172a', fontWeight: 'bold', cursor: 'pointer' 
  }
};

export default Home;