const Contact = () => {
  const pageStyle = {
    textAlign: 'center',
    marginTop: '50px',
    padding: '20px',
    fontFamily: 'sans-serif'
  };

  return (
    <div style={pageStyle}>
      <h1>Contact Us</h1>
      <p>Email: support@ticketmaster.com</p>
      <p>Phone: +994 50 000 00 00</p>
    </div>
  );
};

const styles = {
  page: {
    maxWidth: '800px', margin: '80px auto', padding: '40px',
    background: '#fff', borderRadius: '20px', textAlign: 'center',
    boxShadow: '0 4px 30px rgba(0,0,0,0.03)'
  },
  title: { fontSize: '32px', color: '#1a202c', marginBottom: '20px' },
  text: { fontSize: '18px', color: '#4a5568', lineHeight: '1.8' }
};

export default Contact;