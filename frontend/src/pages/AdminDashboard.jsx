import { useEffect, useState } from 'react';
import api from '../api';

const AdminDashboard = () => {
  const [events, setEvents] = useState([]);
  const [newEvent, setNewEvent] = useState({ name: '', description: '', price: 0 });

  useEffect(() => {
    fetchEvents();
  }, []);

  const fetchEvents = () => {
    api.get('/Events').then(res => setEvents(res.data));
  };

  const handleCreate = async (e) => {
    e.preventDefault();
    try {
      await api.post('/Events', newEvent);
      setNewEvent({ name: '', description: '', price: 0 });
      fetchEvents(); // Siyahını yenilə
      alert("Tədbir uğurla əlavə edildi!");
    } catch (err) {
      console.error(err);
    }
  };

  const handleDelete = async (id) => {
    if(window.confirm("Silmək istədiyinizə əminsiniz?")) {
      await api.delete(`/Events/${id}`);
      fetchEvents();
    }
  };

  return (
    <div style={styles.container}>
      <h1 style={styles.title}>Admin Control Panel</h1>
      
      {/* Yeni Tədbir Əlavə Etmə Formu */}
      <section style={styles.section}>
        <h2>Add New Event</h2>
        <form onSubmit={handleCreate} style={styles.form}>
          <input 
            type="text" placeholder="Event Name" 
            value={newEvent.name} 
            onChange={e => setNewEvent({...newEvent, name: e.target.value})} 
            style={styles.input} required
          />
          <input 
            type="number" placeholder="Price" 
            value={newEvent.price} 
            onChange={e => setNewEvent({...newEvent, price: e.target.value})} 
            style={styles.input} required
          />
          <textarea 
            placeholder="Description" 
            value={newEvent.description} 
            onChange={e => setNewEvent({...newEvent, description: e.target.value})} 
            style={styles.textarea} required
          />
          <button type="submit" style={styles.addBtn}>Save Event</button>
        </form>
      </section>

      {/* Mövcud Tədbirlər Cədvəli */}
      <section style={styles.section}>
        <h2>Manage Events</h2>
        <table style={styles.table}>
          <thead>
            <tr>
              <th style={styles.th}>ID</th>
              <th style={styles.th}>Name</th>
              <th style={styles.th}>Price</th>
              <th style={styles.th}>Actions</th>
            </tr>
          </thead>
          <tbody>
            {events.map(event => (
              <tr key={event.id} style={styles.tr}>
                <td style={styles.td}>{event.id}</td>
                <td style={styles.td}>{event.name}</td>
                <td style={styles.td}>${event.price}</td>
                <td style={styles.td}>
                  <button onClick={() => handleDelete(event.id)} style={styles.delBtn}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </section>
    </div>
  );
};

const styles = {
  container: { padding: '40px 8%', color: '#fff', backgroundColor: '#0f172a', minHeight: '100vh' },
  title: { fontSize: '32px', marginBottom: '30px', color: '#818cf8' },
  section: { background: '#1e293b', padding: '30px', borderRadius: '15px', marginBottom: '40px' },
  form: { display: 'flex', flexDirection: 'column', gap: '15px', maxWidth: '400px' },
  input: { padding: '12px', borderRadius: '8px', border: 'none', background: '#334155', color: '#fff' },
  textarea: { padding: '12px', borderRadius: '8px', border: 'none', background: '#334155', color: '#fff', height: '100px' },
  addBtn: { background: '#10b981', color: '#fff', border: 'none', padding: '12px', borderRadius: '8px', cursor: 'pointer', fontWeight: 'bold' },
  table: { width: '100%', borderCollapse: 'collapse', marginTop: '20px' },
  th: { textAlign: 'left', padding: '15px', borderBottom: '1px solid #334155', color: '#94a3b8' },
  td: { padding: '15px', borderBottom: '1px solid #334155' },
  delBtn: { background: '#ef4444', color: '#fff', border: 'none', padding: '6px 12px', borderRadius: '5px', cursor: 'pointer' }
};

export default AdminDashboard;