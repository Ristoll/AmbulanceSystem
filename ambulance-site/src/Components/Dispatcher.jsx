import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AuthService } from "../api/services/AuthService.js";
import { CallApiClient } from "../api/clients/CallApiClient.js";
import { BrigadeApiClient } from "../api/clients/BrigadeApiClient.js";
import ProfileTab from "./ProfileTab";
import BrigadeCard from "./BrigadeCard";
import CallForm from "./CallForm";
import NotificationModal from "./NotificationModal";
import CallItem from "./CallItem";
import * as signalR from "@microsoft/signalr";
import "./Dispatcher.css";

function Dispatcher() {
  const callApiClient = new CallApiClient(); // створюємо екземпляр API клієнта для викликів
  const brigadeApiClient = new BrigadeApiClient(); // створюємо екземпляр API клієнта для бригад

  const [brigades, setBrigades] = useState([]); // початковий стан бригад
  const [callStartTime, setCallStartTime] = useState(null); // початковий стан часу початку виклику
  const [calls, setCalls] = useState([]); // початковий стан викликів
  const [activeTab, setActiveTab] = useState("calls");
  const [showCallForm, setShowCallForm] = useState(false);
  const [isNotifModalOpen, setIsNotifModalOpen] = useState(false);
  const [notifications, setNotifications] = useState([]);
  const navigate = useNavigate();

  useEffect(() => { // порожній масив залежностей [] = запусти ефект тільки після першого завантаження компонента
    let mounted = true;
    (async () => {
      try {
        const loadedCalls = await callApiClient.loadCalls();
        const loadedBrigades = await brigadeApiClient.getAllBrigades();
        if (!mounted) return;
        setCalls(Array.isArray(loadedCalls) ? loadedCalls : []);
        setBrigades(
          Array.isArray(loadedBrigades)
            ? loadedBrigades.map(normalizeBrigade)
            : []
        );
      } catch (err) {
        console.error("Помилка при завантаженні даних диспетчера", err);
      }
    })();
    return () => {
      mounted = false;
    };
  }, []);

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:7090/notificationHub")
      .withAutomaticReconnect()
      .build();

    connection.start()
      .then(() => console.log("Підключено до SignalR"))
      .catch(err => console.error(err));

    connection.on("ReceiveMessage", (msg) => {
      setNotifications(prev => [...prev, msg]);
    });

    return () => {
      connection.stop();
    };
  }, []);

  const handleOpenForm = () => {
    setCallStartTime(new Date()); // фіксуємо час відкриття форми
    setShowCallForm(true);
  };

  const handleCloseForm = () => {
    setShowCallForm(false);
    setCallStartTime(null); // скидаємо час початку виклику
  };

  const handleSubmitCall = async (callId) => {
    try {
      // підвантажуємо повний об'єкт виклику
      const newCall = await callApiClient.loadCall(callId);
      setCalls((prev) => [newCall, ...prev]);

      // оновлюємо бригади (могли б тільки статус 1, але це більш вигідно, бо реагує на зміний й інших диспетчерів)
      const loadedBrigades = await brigadeApiClient.getAllBrigades();
      setBrigades(
        Array.isArray(loadedBrigades)
          ? loadedBrigades.map(normalizeBrigade)
          : []
      );

      setShowCallForm(false);
      alert("Виклик успішно створено");
    } catch (error) {
      alert("Помилка після створення виклику: " + error.message);
    }
  };

  const normalizeBrigade = (b) => ({
    id: b.brigadeId,
    status: b.brigadeStateCode,
    type: b.brigadeTypeName,
    members: b.members ? b.members.map((m) => m.personFullName) : [],
    hospitalId: b.hospitalId ?? b.HospitalId ?? null,
  });

  const handleDeleteCall = async (callId) => {
    if (!window.confirm("Видалити цей виклик?")) return;

    try {
      console.log("видалення - " + callId);
      await callApiClient.deleteCall(callId);
      setCalls((prev) => // просто щоб завйвий раз не вантажити
        prev.filter((call) => (call.callId ?? call.id) !== callId)
      );

      // оновлюємо бригади (могли б тільки статус 1, але це більш вигідно, бо реагує на зміний й інших диспетчерів)
      const loadedBrigades = await brigadeApiClient.getAllBrigades();
      setBrigades(
        Array.isArray(loadedBrigades)
          ? loadedBrigades.map(normalizeBrigade)
          : []
      );
      alert("Виклик успішно видалено");
    } catch (err) {
      alert("Помилка при видаленні виклику: " + err.message);
    }
  };

  const handleLogout = () => {
    if (window.confirm("Ви впевнені, що хочете вийти з кабінету?")) {
      AuthService.deleteUserData();
      navigate("/start");
    }
  };

  return (
    <div className="dispatcher">
      <div className="tabs">
        <button
          className={activeTab === "calls" ? "active" : ""}
          onClick={() => setActiveTab("calls")}
        >
          Виклики
        </button>

        <button
          className={activeTab === "brigades" ? "active" : ""}
          onClick={() => setActiveTab("brigades")}
        >
          Бригади
        </button>

        <button
          className={activeTab === "notifications" ? "active" : ""}
          onClick={() => setIsNotifModalOpen(true)}
          aria-haspopup="dialog"
        >
          Повідомлення
        </button>

        <button
          className={activeTab === "profile" ? "active" : ""}
          onClick={() => setActiveTab("profile")}
        >
          Профіль
        </button>

        <button className="logout-btn" onClick={handleLogout}>
          ⏎ Вийти
        </button>
      </div>

      <div className="content">
        {activeTab === "calls" && (
          <div className="calls-panel">
            {!showCallForm ? (
              <div className="calls-container">
                <div className="calls-header">
                  <h3>Виклики ({calls.length})</h3>
                  <button className="new-call-btn" onClick={handleOpenForm}>
                    + Створити новий виклик
                  </button>
                </div>

                {calls.length === 0 ? (
                  <div className="empty-calls">
                    <p>Поки що немає активних викликів</p>
                    <p className="empty-hint">
                      Натисніть "Створити новий виклик" для додавання
                    </p>
                  </div>
                ) : (
                  <div className="calls-list">
                    {calls.map((call) => (
                      <CallItem
                        key={call.callId ?? call.id}
                        call={call}
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
                callApiClient={callApiClient}
                callStartTime={callStartTime}
              />
            )}
          </div>
        )}

        {activeTab === "brigades" && (
          <div className="brigades-panel">
            {brigades.map((b) => (
              <BrigadeCard key={b.id} {...b} />
            ))}
          </div>
        )}

        {activeTab === "profile" && <ProfileTab />}

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
