const About = () => {
  const pageStyle = {
    textAlign: 'center',
    marginTop: '50px',
    padding: '20px',
    fontFamily: 'sans-serif'
  };

  return (
    <div style={pageStyle}>
      <h1>About Us</h1>
      <p>We are a leading ticket booking platform with a focus on security and speed.</p>
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

export default About;