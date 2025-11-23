import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthService } from "../api/services/AuthService.js";
import BrigadeCard from './BrigadeCard';
import CallForm from './CallForm';
import NotificationModal from './NotificationModal';
import './Dispatcher.css';
import CallItem from './CallItem';

function Dispatcher() {
  const [brigades, setBrigades] = useState([
    { id: 1, status: 'Вільна', type: 'Швидка', members: ['Іван', 'Петро'] },
    { id: 2, status: 'У виїзді', type: 'Реанімація', members: ['Олег', 'Сергій'] },
    { id: 3, status: 'Вільна', type: 'Звичайна', members: ['Марія', 'Юрій'] },
    { id: 4, status: 'Вільна', type: 'Звичайна', members: ['Анна', 'Богдан'] },
  ]);

  const [calls, setCalls] = useState([]);
  const [notifications, setNotifications] = useState([
    { id: 1, title: 'Тестове повідомлення', message: 'Це статичне тестове повідомлення для демонстрації.', createdAt: new Date().toISOString() },
    { id: 2, title: 'Оновлення системи', message: 'Планове технічне обслуговування о 22:00.', createdAt: new Date().toISOString() },
    { id: 3, title: 'Нагадування', message: 'Не забудьте перевірити обладнання в бригаді №3.', createdAt: new Date().toISOString() }
  ]);
  
  const [activeTab, setActiveTab] = useState('calls');
  const [showCallForm, setShowCallForm] = useState(false);
  const [isNotifModalOpen, setIsNotifModalOpen] = useState(false);
  const navigate = useNavigate();

  const handleCloseForm = () => {
    setShowCallForm(false);
  };

  const handleSubmitCall = (callData) => {
    const newCall = {
      id: -1,
      ...callData,
      createdAt: 1,
      status: 'Новий',
      patient: 'Отримано з ID',
      specification: 'За викликом'
    };
    
    // оновлюємо статус бригади
    setBrigades(prev => prev.map(b => 
      b.id === callData.brigadeId 
        ? { ...b, status: 'У виїзді', currentCallId: newCall.id }
        : b
    ));
    
    setCalls(prev => [newCall, ...prev]);
    setShowCallForm(false);
    alert('Виклик успішно створено');
  };

  const handleDeleteCall = (callId) => {
    if (window.confirm('Видалити цей виклик?')) {
      setCalls(prev => prev.filter(call => call.id !== callId));
    }
  };

  const handleEditCall = (callId) => {
    alert(`Редагування виклику #${callId}`); // тимчасова затичка
  };

  const handleLogout = () => {
    if (window.confirm('Ви впевнені, що хочете вийти з кабінету?')) {
      AuthService.deleteUserData();
      navigate('/start');
    }
  };

  return (
    <div className="dispatcher">
      <div className="tabs">
        <button
          className={activeTab === 'calls' ? 'active' : ''}
          onClick={() => setActiveTab('calls')}
        >
          Виклики
        </button>

        <button
          className={activeTab === 'brigades' ? 'active' : ''}
          onClick={() => setActiveTab('brigades')}
        >
          Бригади
        </button>

        <button
          className={activeTab === 'notifications' ? 'active' : ''}
          onClick={() => setIsNotifModalOpen(true)}
          aria-haspopup="dialog"
        >
          Повідомлення
        </button>

        <button className="logout-btn" onClick={handleLogout}>
          ⏎ Вийти
        </button>
      </div>

      <div className="content">
        {activeTab === 'calls' && (
          <div className="calls-panel">
            {!showCallForm ? (
              <div className="calls-container">
                <div className="calls-header">
                  <h3>Виклики ({calls.length})</h3>
                  <button
                    className="new-call-btn"
                    onClick={() => setShowCallForm(true)}
                  >
                    + Створити новий виклик
                  </button>
                </div>
                
                {calls.length === 0 ? (
                  <div className="empty-calls">
                    <p>Поки що немає активних викликів</p>
                    <p className="empty-hint">Натисніть "Створити новий виклик" для додавання</p>
                  </div>
                ) : (
                  <div className="calls-list">
                    {calls.map(call => (
                      <CallItem 
                        key={call.id} 
                        call={call}
                        onEdit={handleEditCall}
                        onDelete={handleDeleteCall}
                      />
                    ))}
                  </div>
                )}
              </div>
            ) : (
              <CallForm 
                onClose={handleCloseForm}
                onSubmit={handleSubmitCall}
                availableBrigades={brigades}
              />
            )}
          </div>
        )}

        {activeTab === 'brigades' && (
          <div className="brigades-panel">
            {brigades.map(b => (
              <BrigadeCard key={b.id} {...b} />
            ))}
          </div>
        )}

        {isNotifModalOpen && (
          <NotificationModal
            notifications={notifications}
            onClose={() => setIsNotifModalOpen(false)}
          />
        )}
      </div>
    </div>
  );
}

export default Dispatcher;