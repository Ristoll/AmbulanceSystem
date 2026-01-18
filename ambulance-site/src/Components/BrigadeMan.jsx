import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthService } from "../api/services/AuthService.js";
import CurrentCallPanel from './CurrentCallPanel.jsx';
import PatientCardsPanel from './PatientCardsPanel.jsx';
import MedicinesPanel from './MedicinesPanel.jsx';
import './BrigadeMan.css';

function BrigadeMan() {
  const navigate = useNavigate();
  const [activeTab, setActiveTab] = useState('currentCall');
  const [token, setToken] = useState(null);

  // Отримуємо токен при завантаженні
  useEffect(() => {
    const t = AuthService.getToken();
    if (!t) {
      navigate('/start');
    } else {
      setToken(t);
    }
  }, [navigate]);

  const tokenProvider = async () => token;

  const handleLogout = () => {
    if (window.confirm('Ви впевнені, що хочете вийти?')) {
      AuthService.deleteUserData();
      navigate('/start');
    }
  };

  if (!token) return <p>Завантаження...</p>;

  return (
    <div className="brigade-man">
      <div className="tabs">
        <button className={activeTab === 'currentCall' ? 'active' : ''} onClick={() => setActiveTab('currentCall')}>Поточний виклик</button>
        <button className={activeTab === 'patients' ? 'active' : ''} onClick={() => setActiveTab('patients')}>Картки пацієнтів</button>
        <button className={activeTab === 'medicines' ? 'active' : ''} onClick={() => setActiveTab('medicines')}>Медикаменти бригади</button>
        <button className="logout-btn" onClick={handleLogout}>⏎ Вийти</button>
      </div>

      <div className="content">
        {activeTab === 'currentCall' && <CurrentCallPanel apiBaseUrl="http://localhost:7090" tokenProvider={tokenProvider} />}
        {activeTab === 'patients' && <PatientCardsPanel apiBaseUrl="http://localhost:7090" tokenProvider={tokenProvider} />}
        {activeTab === 'medicines' && <MedicinesPanel apiBaseUrl="http://localhost:7090" tokenProvider={tokenProvider} brigadeOnly={true} />}
      </div>
    </div>
  );
}

export default BrigadeMan;
