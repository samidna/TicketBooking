import React, { useEffect, useState } from 'react';
import api from '../api';

const AdminDashboard = () => {
  const [data, setData] = useState([]);
  const [activeTab, setActiveTab] = useState('events');
  const [events, setEvents] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [totalCount, setTotalCount] = useState(0);
  const pageSize = 5;

  useEffect(() => {
    fetchData(currentPage);
  }, [activeTab, currentPage]);

  const fetchData = async () => {
    setLoading(true);
    try {
      const endpoint = (activeTab.toLowerCase() === 'users' ? 'Account' : activeTab.charAt(0).toUpperCase() + activeTab.slice(1));

      const response = await api.get(`${endpoint}?page=${currentPage}&pageSize=${pageSize}`);

      if (response.data && response.data.items) {
        setData(response.data.items);
        setTotalCount(response.data.totalCount);
      } else {
        setData(response.data);
      }
    } catch (err) {
      console.error("Məlumat gətirilərkən xəta:", err);
      setData([]);
    }
    setLoading(false);
  };

  const getColumns = () => {
    if (data.length === 0) return [];
    return Object.keys(data[0]).filter(key => key !== 'password');
  };

  return (
    <div style={styles.wrapper}>
      {/* SIDEBAR */}
      <aside style={styles.sidebar}>
        <h2 style={styles.logo}>TICKET<span style={{ color: '#818cf8' }}>ADMIN</span></h2>
        <nav>
          <div
            onClick={() => { setActiveTab('events'); setCurrentPage(1); }}
            style={activeTab === 'events' ? styles.activeItem : styles.menuItem}>
            📅 Events
          </div>
          <div
            onClick={() => { setActiveTab('users'); setCurrentPage(1); }}
            style={activeTab === 'users' ? styles.activeItem : styles.menuItem}>
            👥 Users
          </div>
          <div
            onClick={() => { setActiveTab('tickets'); setCurrentPage(1); }}
            style={activeTab === 'tickets' ? styles.activeItem : styles.menuItem}>
            🎫 Tickets (Transactions)
          </div>
          <div
            onClick={() => { setActiveTab('blogs'); setCurrentPage(1); }}
            style={activeTab === 'blogs' ? styles.activeItem : styles.menuItem}>
            ✍️ Blog Posts
          </div>
        </nav>
      </aside>

      {/* MAIN CONTENT */}
      <main style={styles.main}>
        <header style={styles.header}>
          <h2>{activeTab.toUpperCase()} Management</h2>
          <button style={styles.addBtn}>+ Add New {activeTab.slice(0, -1)}</button>
        </header>

        <div style={styles.tableCard}>
          {/* {loading ? <p>Yüklənir...</p> : ( */}
            <table style={styles.table}>
              <thead>
                <tr>
                  {getColumns().map(col => (
                    <th key={col} style={styles.th}>{col.toUpperCase()}</th>
                  ))}
                  <th style={styles.th}>ACTIONS</th>
                </tr>
              </thead>
              <tbody>
                {data.map((item, index) => (
                  <tr key={index} style={styles.tr}>
                    {getColumns().map(col => (
                      <td key={col} style={styles.td}>
                        {typeof item[col] === 'object' ? 'Link/Object' : String(item[col])}
                      </td>
                    ))}
                    <td style={styles.td}>
                      <button style={styles.editBtn}>Edit</button>
                      <button style={styles.delBtn}>Delete</button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          {/* )} */}

          {/* PAGINATION */}
          <div style={styles.pagination}>
            <button disabled={currentPage === 1} onClick={() => setCurrentPage(p => p - 1)} style={styles.pageBtn}>Prev</button>
            <span style={{ color: '#fff' }}>Page {currentPage}</span>
            <button onClick={() => setCurrentPage(p => p + 1)} style={styles.pageBtn}>Next</button>
          </div>
        </div>
      </main>
    </div>
  );
};

const styles = {
  wrapper: { display: 'flex', minHeight: '100vh', background: '#0f172a', fontFamily: 'Poppins, sans-serif' },
  sidebar: { width: '250px', background: '#1e293b', padding: '30px 15px', borderRight: '1px solid #334155' },
  logo: { color: '#fff', textAlign: 'center', marginBottom: '40px', fontSize: '20px' },
  menuItem: { fontSize: '12px', padding: '12px 20px', color: '#94a3b8', cursor: 'pointer', borderRadius: '8px', marginBottom: '10px' },
  activeItem: { fontSize: '14px', padding: '12px 20px', color: '#fff', background: '#334155', cursor: 'pointer', borderRadius: '8px', marginBottom: '10px', fontWeight: 'bold' },
  main: { flex: 1, padding: '40px' },
  header: { display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '30px', color: '#fff' },
  addBtn: { background: '#6366f1', color: '#fff', border: 'none', padding: '10px 20px', borderRadius: '8px', cursor: 'pointer' },
  tableCard: { background: '#1e293b', padding: '20px', borderRadius: '15px', overflowX: 'auto' },
  table: { width: '100%', borderCollapse: 'collapse', color: '#cbd5e1' },
  th: { textAlign: 'left', padding: '12px', borderBottom: '2px solid #334155', fontSize: '13px' },
  td: { padding: '12px', borderBottom: '1px solid #334155', fontSize: '14px' },
  editBtn: { background: '#f59e0b', border: 'none', padding: '5px 10px', borderRadius: '4px', marginRight: '5px', cursor: 'pointer' },
  delBtn: { background: '#ef4444', border: 'none', padding: '5px 10px', borderRadius: '4px', color: '#fff', cursor: 'pointer' },
  pagination: { display: 'flex', justifyContent: 'center', marginTop: '20px', gap: '20px', alignItems: 'center' },
  pageBtn: { background: '#334155', color: '#fff', border: 'none', padding: '8px 15px', borderRadius: '5px', cursor: 'pointer' }
};

export default AdminDashboard;