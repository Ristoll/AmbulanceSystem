import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AuthService } from "../api/services/AuthService.js";
import { PersonApiClient } from "../api/clients/PersonApiClient.js";
import ProfileTab from "./ProfileTab";
import CallItem from "./CallItem";
import NotificationModal from "./NotificationModal";
import * as signalR from "@microsoft/signalr";
import "./Patient.css";

function Patient() {
  const navigate = useNavigate();

  const [activeTab, setActiveTab] = useState("profile");
  const [medicalCard, setMedicalCard] = useState(null);
  const [calls, setCalls] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isNotifModalOpen, setIsNotifModalOpen] = useState(false);
  const [notifications, setNotifications] = useState([]);

  const personApiClient = new PersonApiClient();

  useEffect(() => {
    loadPatientData();
  }, []);

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:7090/notificationHub", {
      accessTokenFactory: () => {
        const token = AuthService.getToken();
        console.log("SignalR токен:", token); // для дебагу
        return token;
      }
    })
    .withAutomaticReconnect()
    .build();

    connection.start()
      .then(() => console.log("Пацієнт: Підключено до SignalR"))
      .catch(err => console.error("Помилка підключення SignalR:", err));

    connection.on("ReceiveMessage", (msg) => {
      setNotifications(prev => [...prev, msg]);
    });

    return () => {
      connection.stop();
    };
  }, []);

  const loadPatientData = async () => {
    try {
      setLoading(true);
      const personId = AuthService.getPersonId();

      if (!personId) {
        throw new Error("Не вдалося отримати ID користувача");
      }

      // вантажимо медичну картку
      const patientInformation = await personApiClient.getPatientData(personId);
      console.log(patientInformation);
      setMedicalCard(patientInformation.medicalCard);

      // вантажимо виклики пацієнта
      setCalls(patientInformation.calls);
    } catch (err) {
      setError(err.message);
      console.error("Помилка завантаження даних пацієнта:", err);
    } finally {
      setLoading(false);
    }
  };

  const handleLogout = () => {
    if (window.confirm("Ви впевнені, що хочете вийти з кабінету?")) {
      AuthService.deleteUserData();
      navigate("/start");
    }
  };

  if (loading)
    return <div className="patient-loading">Завантаження даних...</div>;
  if (error) return <div className="patient-error">Помилка: {error}</div>;

  return (
    <div className="patient">
      <div className="tabs">
        <button
          className={activeTab === "profile" ? "active" : ""}
          onClick={() => setActiveTab("profile")}
        >
          Профіль
        </button>

        <button
          className={activeTab === "medical" ? "active" : ""}
          onClick={() => setActiveTab("medical")}
        >
          Медична картка
        </button>

        <button
          className={activeTab === "calls" ? "active" : ""}
          onClick={() => setActiveTab("calls")}
        >
          Мої виклики ({calls.length})
        </button>

        <button
          className={activeTab === "notifications" ? "active" : ""}
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
        {activeTab === "profile" && (
          <div className="patient-grid">
            <div className="patient-left">
              <ProfileTab />
            </div>

            <div className="patient-right">
              <h3>5 ваших останніх викликів</h3>
              <div className="calls-list">
                {calls.length > 0 ? (
                  calls.slice(0, 5).map(
                    (
                      call // показуємо лише 5 останніх викликів - модемо робити так, бо вони сортуютсья на беці
                    ) => (
                      <CallItem
                        key={call.callId || call.id}
                        call={call}
                        showActions={false}
                      />
                    )
                  )
                ) : (
                  <div className="no-calls">
                    <p>У вас поки що немає викликів</p>
                  </div>
                )}
              </div>
            </div>
          </div>
        )}

        {activeTab === "medical" && (
          <div className="medical-card-panel">
            <h3>Медична картка</h3>
            {medicalCard ? (
              <div className="medical-card-grid">
                <div>
                  <div className="medical-card-section">
                    <h4>Загальна інформація</h4>
                    <div className="patient-info-grid">
                      <div className="info-item">
                        <span className="info-label">Група крові</span>
                        <span className="info-value">
                          {medicalCard.bloodType || "Не вказано"}
                        </span>
                      </div>
                      <div className="info-item">
                        <span className="info-label">Зріст</span>
                        <span className="info-value">
                          {medicalCard.height
                            ? `${medicalCard.height} см`
                            : "Не вказано"}
                        </span>
                      </div>
                      <div className="info-item">
                        <span className="info-label">Вага</span>
                        <span className="info-value">
                          {medicalCard.weight
                            ? `${medicalCard.weight} кг`
                            : "Не вказано"}
                        </span>
                      </div>
                      <div className="info-item">
                        <span className="info-label">Дата створення</span>
                        <span className="info-value">
                          {medicalCard.creationDate
                            ? new Date(
                                medicalCard.creationDate
                              ).toLocaleDateString("uk-UA")
                            : "Не вказано"}
                        </span>
                      </div>
                    </div>
                  </div>

                  <div className="medical-card-section">
                    <h4>Алергії</h4>
                    <div className="allergies-list">
                      {medicalCard.allergies &&
                      medicalCard.allergies.length > 0 ? (
                        medicalCard.allergies.map((allergy, index) => (
                          <div key={index} className="allergy-item">
                            {allergy}
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
                      {medicalCard.chronicDiseases &&
                      medicalCard.chronicDiseases.length > 0 ? (
                        medicalCard.chronicDiseases.map((disease, index) => (
                          <div key={index} className="disease-item">
                            {disease}
                          </div>
                        ))
                      ) : (
                        <div className="no-data">
                          Хронічні захворювання відсутні
                        </div>
                      )}
                    </div>
                  </div>
                </div>

                <div>
                  <div className="medical-card-section">
                    <h4>Примітки</h4>
                    <div className="info-item">
                      <span className="info-value">
                        {medicalCard.notes || "Примітки відсутні"}
                      </span>
                    </div>
                  </div>

                  <div className="medical-card-section">
                    <h4>Медичні записи</h4>
                    <div className="medical-records-list">
                      {medicalCard.medicalRecords &&
                      medicalCard.medicalRecords.length > 0 ? (
                        medicalCard.medicalRecords.map((record) => (
                          <div
                            key={record.medicalRecordId}
                            className="medical-record-item"
                          >
                            <div className="record-header">
                              <span className="record-doctor">
                                {record.brigadeMemberName ? record.brigadeMemberName : "Лікар"}
                              </span>
                              <span className="record-date">
                                {new Date(record.dataTime).toLocaleDateString(
                                  "uk-UA"
                                )}
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
            ) : (
              <div className="no-medical-card">
                <p>Медична картка не знайдена</p>
                <p className="hint">
                  Зверніться до адміністратора для створення медичної картки
                </p>
              </div>
            )}
          </div>
        )}

        {activeTab === "calls" && (
          <div className="calls-panel">
            <h3>Мої виклики ({calls.length})</h3>
            <div className="calls-list">
              {calls.length > 0 ? (
                calls.map((call) => (
                  <CallItem
                    key={call.callId || call.id}
                    call={call}
                    showActions={false}
                  />
                ))
              ) : (
                <div className="no-calls">
                  <p>У вас поки що немає викликів</p>
                </div>
              )}
            </div>
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

export default Patient;
