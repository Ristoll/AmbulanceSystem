import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import CallItem from './CallItem';
import './Patient.css';

function Patient() {
  const navigate = useNavigate();

  const [activeTab, setActiveTab] = useState('map');

  const [calls] = useState([
    {
      id: 1001,
      phone: '+380501112233',
      patient: 'Петро Іваненко',
      name: 'Петро',
      surname: 'Іваненко',
      middlename: '',
      age: 45,
      specification: 'Швидка допомога',
      description: 'Біль у грудях',
      createdAt: new Date().toLocaleString(),
      status: 'Оброблено'
    }
  ]);

  const [doctors] = useState([
    { id: 1, name: 'Д-р Олег Петров', specialty: 'Кардіолог', phone: '+380631112233' },
    { id: 2, name: 'Д-р Наталія Стеценко', specialty: 'Терапевт', phone: '+380631114455' }
  ]);

  // Дані медичної картки
  const [medicalCard] = useState({
    medicalCardId: 1,
    personId: 1,
    creationDate: new Date('2020-05-15'),
    bloodType: 'A+',
    height: 175.5,
    weight: 70.2,
    notes: 'Пацієнт страждає на сезонні алергії. Регулярні огляди.',
    medicalRecords: [
      {
        medicalRecordId: 1,
        medicalCardId: 1,
        brigadeMemberId: 1,
        dataTime: new Date('2024-01-15'),
        diagnoses: 'Гострий бронхіт',
        symptoms: 'Кашель, підвищена температура, задишка',
        treatment: 'Антибіотики, протизапальні препарати, постельний режим',
        imageUrl: null,
        brigadeMember: { name: 'Д-р Іваненко О.П.' }
      },
      {
        medicalRecordId: 2,
        medicalCardId: 1,
        brigadeMemberId: 2,
        dataTime: new Date('2024-03-20'),
        diagnoses: 'Гіпертонія',
        symptoms: 'Головний біль, підвищений тиск',
        treatment: 'Гіпотензивні препарати, дієта, контроль ваги',
        imageUrl: null,
        brigadeMember: { name: 'Д-р Петрова М.І.' }
      }
    ],
    patientAllergies: [
      { allergyId: 1, name: 'Пилок берези' },
      { allergyId: 2, name: 'Пеніцилін' }
    ],
    patientChronicDeceases: [
      { deceaseId: 1, name: 'Гіпертонія' },
      { deceaseId: 2, name: 'Астма' }
    ],
    person: {
      firstName: 'Петро',
      lastName: 'Іваненко',
      birthDate: new Date('1979-05-20')
    }
  });

  const calculateAge = (birthDate) => {
    const today = new Date();
    const birth = new Date(birthDate);
    let age = today.getFullYear() - birth.getFullYear();
    const monthDiff = today.getMonth() - birth.getMonth();
    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) {
      age--;
    }
    return age;
  };

  return (
    <div className="patient">
      <div className="tabs">
        <button
          className={activeTab === 'map' ? 'active' : ''}
          onClick={() => setActiveTab('map')}
        >
          Карта та лікарі
        </button>

        <button
          className={activeTab === 'medical' ? 'active' : ''}
          onClick={() => setActiveTab('medical')}
        >
          Медична картка
        </button>

        <button
          className={activeTab === 'calls' ? 'active' : ''}
          onClick={() => setActiveTab('calls')}
        >
          Мої виклики
        </button>

        <button className="logout-btn" onClick={() => {
          if (window.confirm('Вийти?')) navigate('/');
        }}>
          ⏎ Вийти
        </button>
      </div>

      <div className="content">
        {activeTab === 'map' && (
          <div className="map-layout">
            <section className="map-panel">
              <h3>Карта</h3>
              <div className="map-placeholder">Тут буде інтерактивна карта</div>
            </section>

            <aside className="doctors-panel">
              <h3>Лікарі</h3>
              <div className="doctors-list">
                {doctors.map(d => (
                  <div key={d.id} className="doctor-card">
                    <div className="doctor-name">{d.name}</div>
                    <div className="doctor-spec">{d.specialty}</div>
                    <div className="doctor-phone">{d.phone}</div>
                  </div>
                ))}
              </div>
            </aside>
          </div>
        )}

        {activeTab === 'medical' && (
          <div className="medical-card-panel">
            <h3>Медична картка</h3>
            <div className="medical-card-grid">
              <div>
                <div className="medical-card-section">
                  <h4>Загальна інформація</h4>
                  <div className="patient-info-grid">
                    <div className="info-item">
                      <span className="info-label">ПІБ</span>
                      <span className="info-value">
                        {medicalCard.person.lastName} {medicalCard.person.firstName}
                      </span>
                    </div>
                    <div className="info-item">
                      <span className="info-label">Вік</span>
                      <span className="info-value">
                        {calculateAge(medicalCard.person.birthDate)} років
                      </span>
                    </div>
                    <div className="info-item">
                      <span className="info-label">Група крові</span>
                      <span className="info-value">{medicalCard.bloodType || 'Не вказано'}</span>
                    </div>
                    <div className="info-item">
                      <span className="info-label">Зріст</span>
                      <span className="info-value">{medicalCard.height ? `${medicalCard.height} см` : 'Не вказано'}</span>
                    </div>
                    <div className="info-item">
                      <span className="info-label">Вага</span>
                      <span className="info-value">{medicalCard.weight ? `${medicalCard.weight} кг` : 'Не вказано'}</span>
                    </div>
                    <div className="info-item">
                      <span className="info-label">Дата створення</span>
                      <span className="info-value">
                        {medicalCard.creationDate ? new Date(medicalCard.creationDate).toLocaleDateString('uk-UA') : 'Не вказано'}
                      </span>
                    </div>
                  </div>
                </div>

                <div className="medical-card-section">
                  <h4>Алергії</h4>
                  <div className="allergies-list">
                    {medicalCard.patientAllergies.length > 0 ? (
                      medicalCard.patientAllergies.map(allergy => (
                        <div key={allergy.allergyId} className="allergy-item">
                          {allergy.name}
                        </div>
                      ))
                    ) : (
                      <div className="no-data">Алергії відсутні</div>
                    )}
                  </div>
                </div>

                <div className="medical-card-section">
                  <h4>Хронічні захворювання</h4>
                  <div className="chronic-diseases-list">
                    {medicalCard.patientChronicDeceases.length > 0 ? (
                      medicalCard.patientChronicDeceases.map(disease => (
                        <div key={disease.deceaseId} className="disease-item">
                          {disease.name}
                        </div>
                      ))
                    ) : (
                      <div className="no-data">Хронічні захворювання відсутні</div>
                    )}
                  </div>
                </div>
              </div>

              <div>
                <div className="medical-card-section">
                  <h4>Примітки</h4>
                  <div className="info-item">
                    <span className="info-value">
                      {medicalCard.notes || 'Примітки відсутні'}
                    </span>
                  </div>
                </div>

                <div className="medical-card-section">
                  <h4>Медичні записи</h4>
                  <div className="medical-records-list">
                    {medicalCard.medicalRecords.length > 0 ? (
                      medicalCard.medicalRecords.map(record => (
                        <div key={record.medicalRecordId} className="medical-record-item">
                          <div className="record-header">
                            <span className="record-doctor">{record.brigadeMember.name}</span>
                            <span className="record-date">
                              {new Date(record.dataTime).toLocaleDateString('uk-UA')}
                            </span>
                          </div>
                          <div className="record-details">
                            {record.diagnoses && (
                              <div className="record-detail">
                                <strong>Діагноз:</strong> {record.diagnoses}
                              </div>
                            )}
                            {record.symptoms && (
                              <div className="record-detail">
                                <strong>Симптоми:</strong> {record.symptoms}
                              </div>
                            )}
                            {record.treatment && (
                              <div className="record-detail">
                                <strong>Лікування:</strong> {record.treatment}
                              </div>
                            )}
                          </div>
                        </div>
                      ))
                    ) : (
                      <div className="no-data">Медичні записи відсутні</div>
                    )}
                  </div>
                </div>
              </div>
            </div>
          </div>
        )}

        {activeTab === 'calls' && (
          <div className="calls-panel">
            <h3>Мої виклики ({calls.length})</h3>
            <div className="calls-list">
              {calls.map(c => (
                <CallItem key={c.id} call={c} showActions={false} />
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

export default Patient;