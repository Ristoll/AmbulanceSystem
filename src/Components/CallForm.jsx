import { useState } from 'react';
import './CallForm.css';
import CallMap from './CallMap';

function CallForm({ onClose, onSubmit, availableBrigades = [] }) {
  const [routeDistance, setRouteDistance] = useState(null);
  const [routeDuration, setRouteDuration] = useState(null);
  const [callData, setCallData] = useState({
    patientId: null,
    phone: '',
    urgencyType: 1,
    address: '',
    notes: '',
    selectedBrigadeId: null,
    hospitalId: null
  });

  const [addressError, setAddressError] = useState('');

  const patientsList = [
    { id: 1, name: 'Іван Іванов', phone: '+380501112233' },
    { id: 2, name: 'Петро Петров', phone: '+380502223344' },
    { id: 3, name: 'Марія Марієнко', phone: '+380503334455' }
  ];

  const hospitalsList = [
    { id: 1, name: 'Центральна міська лікарня', address: 'Київ, вул. Хрещатик, 1' },
    { id: 2, name: 'Обласна клінічна лікарня', address: 'Київ, вул. Мечникова, 5' },
    { id: 3, name: 'Лікарня швидкої допомоги', address: 'Київ, вул. Братська, 5' }
  ];

  const urgencyLevels = [
    { value: 1, label: 'Низька', description: 'Плановий виклик', className: 'low' },
    { value: 2, label: 'Середня', description: 'Терміновий виклик', className: 'medium' },
    { value: 3, label: 'Висока', description: 'Екстрений виклик', className: 'high' }
  ];

  const validateAddress = (address) => {
    if (!address) {
      setAddressError('');
      return true;
    }

    // перевірка формату адреси (місто, вулиця, номер будинку)
    const addressPattern = /^(.+),\s*(вул\.|пров\.|просп\.|бульв\.|пл\.)?\s*(.+),\s*(\d+[a-zA-Z]?)$/i;
    if (!addressPattern.test(address)) {
      setAddressError('Будь ласка, введіть повну адресу у форматі: "Місто, вул. Назва, номер"');
      return false;
    }
    
    setAddressError('');
    return true;
  };

  const getHospitalAddress = () => {
    if (!callData.hospitalId) return null;
    const hospital = hospitalsList.find(h => h.id === parseInt(callData.hospitalId));
    return hospital ? hospital.address : null;
  };

  const handleInputChange = (field, value) => {
    setCallData(prev => ({
      ...prev,
      [field]: value
    }));

    // Валідація адреси при зміні
    if (field === 'address') {
      validateAddress(value);
    }
  };

  const handlePatientSelect = (patientId) => {
    if (patientId === 'new') {
      setCallData(prev => ({
        ...prev,
        patientId: null,
        phone: ''
      }));
      return;
    }

    const patient = patientsList.find(p => p.id === parseInt(patientId));
    setCallData(prev => ({
      ...prev,
      patientId: patientId,
      phone: patient ? patient.phone : ''
    }));
  };

  const handleBrigadeSelect = (brigadeId) => {
    setCallData(prev => ({
      ...prev,
      selectedBrigadeId: brigadeId
    }));
  };

  const handleUrgencySelect = (urgency) => {
    setCallData(prev => ({
      ...prev,
      urgencyType: urgency
    }));
  };

  const handleSubmit = () => {
    if (!callData.selectedBrigadeId) {
      alert('Будь ласка, оберіть бригаду');
      return;
    }

    if (callData.address && !validateAddress(callData.address)) {
      return;
    }

    const submitData = { // прост тимчасова затичка, потім при реалізації змінемо
      patientId: callData.patientId ? parseInt(callData.patientId) : null,
      phone: callData.phone,
      urgencyType: callData.urgencyType,
      address: callData.address,
      notes: callData.notes,
      brigadeId: callData.selectedBrigadeId,
      hospitalId: callData.hospitalId ? parseInt(callData.hospitalId) : null,
    };

    if (onSubmit) {
      onSubmit(submitData);
    } else {
      onClose();
      alert('Виклик успішно створено');
    }
  };

  const availableBrigadesList = availableBrigades.filter(b => b.status === 'Вільна');
  const hospitalAddress = getHospitalAddress();

  // Приклади правильних адрес для підказки
  const addressExamples = [
    'Київ, вул. Хрещатик, 25',
    'Львів, вул. Стрийська, 45',
    'Одеса, вул. Дерибасівська, 12'
  ];

  return (
    <div className="call-form">
      <h3>Новий виклик</h3>
      
      <div className="form-row">
        <div className="form-group">
          <label>Пацієнт:</label>
          <select
            value={callData.patientId !== null ? callData.patientId : 'new'}
            onChange={(e) => handlePatientSelect(e.target.value)}
          >
            <option value="">Виберіть пацієнта</option>
            <option value="new">Нова людина</option>
            {patientsList.map(patient => (
              <option key={patient.id} value={patient.id}>
                {patient.name}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group">
          <label>Номер телефону:</label>
          <input 
            type="tel"
            value={callData.phone}
            onChange={(e) => handleInputChange('phone', e.target.value)}
            placeholder="+380501112233"
          />
        </div>
      </div>

      <div className="form-group full-width">
        <label>Рівень терміновості:</label>
        <div className="urgency-levels">
          {urgencyLevels.map(level => (
            <div
              key={level.value}
              className={`urgency-option urgency-${level.className} ${
                callData.urgencyType === level.value ? 'selected' : ''
              }`}
              onClick={() => handleUrgencySelect(level.value)}
            >
              <div>{level.label}</div>
              <div style={{ fontSize: '0.8rem', color: '#6b7280' }}>
                {level.description}
              </div>
            </div>
          ))}
        </div>
      </div>

      <div className="form-group full-width">
        <label>Адреса виклику:</label>
        <input 
          type="text"
          value={callData.address}
          onChange={(e) => handleInputChange('address', e.target.value)}
          placeholder="Наприклад: Київ, вул. Хрещатик, 25"
          className={`full-input ${addressError ? 'input-error' : ''}`}
        />
        {addressError && (
          <div className="error-message">{addressError}</div>
        )}
        <div className="address-hint">
          <strong>Приклади правильного формату:</strong>
          <ul>
            {addressExamples.map((example, index) => (
              <li key={index}>{example}</li>
            ))}
          </ul>
        </div>
      </div>

      <div className="form-group full-width">
        <label>Лікарня:</label>
        <select
          value={callData.hospitalId || ''}
          onChange={(e) => handleInputChange('hospitalId', e.target.value)}
          className="full-input"
        >
          <option value="">Не призначати лікарню</option>
          {hospitalsList.map(hospital => (
            <option key={hospital.id} value={hospital.id}>
              {hospital.name}
            </option>
          ))}
        </select>
      </div>

      <div className="form-group full-width">
        <label>Примітки:</label>
        <textarea 
          value={callData.notes}
          onChange={(e) => handleInputChange('notes', e.target.value)}
          placeholder="Додаткові примітки щодо виклику"
          rows="3"
          className="full-textarea"
        />
      </div>

      <div className="brigades-selection">
        <h4>Оберіть бригаду</h4>
        <div className="brigades-list">
          {availableBrigadesList.length > 0 ? (
            availableBrigadesList.map(brigade => (
              <div
                key={brigade.id}
                className={`brigade-option ${
                  callData.selectedBrigadeId === brigade.id ? 'selected' : ''
                }`}
                onClick={() => handleBrigadeSelect(brigade.id)}
              >
                <div className="brigade-info">
                  <div className="brigade-type">Бригада #{brigade.id} - {brigade.type}</div>
                  <div className="brigade-members">
                    {brigade.members.join(', ')}
                  </div>
                  <span className={`brigade-status status-${brigade.status === 'Вільна' ? 'available' : 'busy'}`}>
                    {brigade.status}
                  </span>
                </div>
              </div>
            ))
          ) : (
            <div style={{ gridColumn: '1 / -1', textAlign: 'center', color: '#6b7280' }}>
              Немає вільних бригад
            </div>
          )}
        </div>
      </div>

      <div className="map-placeholder">
        {callData.address && hospitalAddress ? (
          <CallMap
            startAddress={callData.address}
            endAddress={hospitalAddress}
            onRouteFound={({ distance, duration }) => {
              setRouteDistance(distance);
              setRouteDuration(duration);
            }}
          />
        ) : (
          <div className="map-placeholder-text">
            {!callData.address && !hospitalAddress ? (
              <p>Введіть адресу виклику та оберіть лікарню для відображення мапи</p>
            ) : !callData.address ? (
              <p>Введіть адресу виклику для відображення мапи</p>
            ) : (
              <p>Оберіть лікарню для відображення мапи</p>
            )}
          </div>
        )}
      </div>

      <div className="distribution-info">
        <div className="info-row">
          <span>Орієнтовний час прибуття:</span>
          <span className="time-value">
            {routeDuration ? Math.round(routeDuration / 60) + " хв" : "~ ..."}
          </span>
        </div>

        <div className="info-row">
          <span>Відстань до лікарні:</span>
          <span className="distance-value">
            {routeDistance ? (routeDistance / 1000).toFixed(1) + " км" : "~ ..."}
          </span>
        </div>
      </div>

      <div className="form-actions">
        <button type="button" className="cancel-btn" onClick={onClose}>
          Скасувати
        </button>
        <button 
          type="button" 
          className="submit-call"
          onClick={handleSubmit}
          disabled={!callData.phone || !callData.selectedBrigadeId || !!addressError}
        >
          Створити виклик
        </button>
      </div>
    </div>
  );
}

export default CallForm;