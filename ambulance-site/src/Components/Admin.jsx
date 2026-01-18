import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthService } from "../api/services/AuthService.js";
import BrigadesPanel from './BrigadesPanel';
import AnalyticsPanel from './AnalyticsPanel.jsx';
import CallsPanel from './CallsPanel.jsx';
import MedicinesPanel from './MedicinesPanel.jsx';
import './Admin.css';
import './AnalyticsPanel.css';
import './BrigadesPanel.css';
import './CallsPanel.css';
import './MedicinesPanel.css';

function Admin() {
  const navigate = useNavigate();
  const [activeTab, setActiveTab] = useState('statistics');
  const [token, setToken] = useState(null);

  // Отримуємо токен при завантаженні адмінки
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
    if (window.confirm('Ви впевнені, що хочете вийти з кабінету?')) {
      AuthService.deleteUserData();
      navigate('/start');
    }
  };

  // Чекаємо токен перед рендером панелей
  if (!token) {
    return <p>Завантаження...</p>;
  }

  return (
    <div className="admin">
      <div className="tabs">
        <button
          className={activeTab === 'statistics' ? 'active' : ''}
          onClick={() => setActiveTab('statistics')}
        >
          Статистика
        </button>
        <button
          className={activeTab === 'brigades' ? 'active' : ''}
          onClick={() => setActiveTab('brigades')}
        >
          Бригади
        </button>
        <button
          className={activeTab === 'calls' ? 'active' : ''}
          onClick={() => setActiveTab('calls')}
        >
          Виклики
        </button>
        <button
          className={activeTab === 'medicines' ? 'active' : ''}
          onClick={() => setActiveTab('medicines')}
        >
          Медикаменти
        </button>
        <button className="logout-btn" onClick={handleLogout}>
          ⏎ Вийти
        </button>
      </div>

      <div className="content">
        {activeTab === 'statistics' && <AnalyticsPanel tokenProvider={tokenProvider} />}
        {activeTab === 'brigades' && (
          <div>
            <BrigadesPanel apiBaseUrl="http://localhost:7090" tokenProvider={tokenProvider} />
          </div>
        )}
        {activeTab === 'calls' && (
          <div>
            <CallsPanel apiBaseUrl="http://localhost:7090" tokenProvider={tokenProvider} />
          </div>
        )}
        {activeTab === 'medicines' && (
          <div>
            <MedicinesPanel apiBaseUrl="http://localhost:7090" tokenProvider={tokenProvider} />
          </div>
        )}
      </div>
    </div>
  );
}

export default Admin;
