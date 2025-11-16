import './App.css';
import Header from './Components/Header';
import HeroSection from './Components/HeroSection';
import Footer from './Components/Footer';
import Modal from './Components/Modal';
import AuthForm from './Components/AuthForm';
import Dispatcher from './Components/Dispatcher';
import Patient from './Components/Patient';
import { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate, useNavigate } from 'react-router-dom';
import 'leaflet/dist/leaflet.css';
import 'leaflet-routing-machine/dist/leaflet-routing-machine.css';

/* обгортка App для використання хука useNavigate всередині App */
function AppWrapper() {
  return (
    <Router>
      <App />
    </Router>
  );
}

function App() {
  const [isLoginOpen, setLoginOpen] = useState(false);
  const [isRegisterOpen, setRegisterOpen] = useState(false);
  const navigate = useNavigate(); // тепер працює, бо App всередині Router

  const handleLoginSuccess = () => {
    navigate('/dispatcher');
  };

  return (
      <div className="App">
        <Routes>
          <Route path="/start" element={
            <>
              <Header 
                onLoginOpen={() => setLoginOpen(true)}
                onRegisterOpen={() => setRegisterOpen(true)}
              />
              <HeroSection />
              <Footer />
            </>
          } />
          
          <Route path="/dispatcher" element={<Dispatcher />} />
          <Route path="/patient" element={<Patient />} />
          
          <Route path="*" element={<Navigate to="/start" replace />} />
        </Routes>

        {/* Модальні вікна */}
        <Modal isOpen={isLoginOpen} onClose={() => { handleLoginSuccess(); setLoginOpen(false); }} contentClassName="modal-small">
          <AuthForm type="login" />
        </Modal>

        <Modal isOpen={isRegisterOpen} onClose={() => setRegisterOpen(false)} contentClassName="modal-small">
          <AuthForm type="register" />
        </Modal>
      </div>
  );
}

export default AppWrapper;