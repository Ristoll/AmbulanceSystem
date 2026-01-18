import './App.css';
import Header from './Components/Header';
import HeroSection from './Components/HeroSection';
import Footer from './Components/Footer';
import Modal from './Components/Modal';
import AuthForm from './Components/AuthForm';
import Dispatcher from './Components/Dispatcher';
import Patient from './Components/Patient';
import Admin from './Components/Admin';
import BrigadeMan from './Components/BrigadeMan';
import { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate, useNavigate } from 'react-router-dom';
import { PersonApiClient } from "./api/clients/PersonApiClient.js";
import { AuthService } from "./api/services/AuthService.js";
import { AuthResponseModel } from "./api/models/PersonModels/AuthResponseModel.js";
import { LoginRequestModel } from "./api/models/PersonModels/LoginRequestModel.js";

/* обгортка App для використання хука useNavigate всередині App (без цього не запрацює) */
function AppWrapper() {
  return (
    <Router>
      <App />
    </Router>
  );
}

function App() {
  const [isLoginOpen, setLoginOpen] = useState(false); // стан модального вікна логіну
  const navigate = useNavigate(); // тепер працює, бо App всередині Router
  const apiClient = new PersonApiClient(); // створюємо екземпляр API клієнта

  const testhandleLoginSuccess = () => {
    navigate('/patient');
  }

  // функція для переходу після успішного логіну
  const handleLoginSuccess = () => {
    const role = AuthService.getUserInfo();
    console.log(role);
    switch(role.userRole) {
        case "Admin": navigate('/admin'); break;
        case "Dispatcher": navigate('/dispatcher'); break;
        case "Patient": navigate('/patient'); break;
        case "BrigadeMan": navigate('/brigadeMan'); break;
        default: navigate('/start');
    }
  };

  // функція для обробки логіну
  const handleLogin = async (login, password) => {
    try {
      const loginRequest = new LoginRequestModel(login, password); // створюємо модель для API

      const authResponseData = await apiClient.authenticate(loginRequest); // виклик API

      if (!authResponseData) {
        alert("Невірний логін або пароль"); // якщо API повернув null
        return;
      }
      console.log(authResponseData);
      const authResponse = new AuthResponseModel(authResponseData); // обгортаємо відповідь у модель
      console.log(authResponse);
      // зберігаємо токен і інформацію про юзера у браузері
      AuthService.saveToken(authResponse.jwtToken);
      AuthService.saveUserInfo(authResponse.login, authResponse.userRole, authResponse.personId);

      // закриваємо нашу модалку для логіну та запускаємо успіх
      setLoginOpen(false);
      handleLoginSuccess();
    } catch (error) {
      alert(error.message); // обробка помилок
    }
  };

  return (
    <div className="App">
      <Routes>
        <Route path="/start" element={
          <>
            <Header
              onLoginOpen={() => setLoginOpen(true)} // відкриваємо модалку логіну
            />
            <HeroSection />
            <Footer />
          </>
        } />

        <Route path="/dispatcher" element={<Dispatcher />} />
        <Route path="/patient" element={<Patient />} />
        <Route path="/admin" element={<Admin />} />
        <Route path="/brigadeMan" element={<BrigadeMan />} />
        <Route path="*" element={<Navigate to="/start" replace />} />
      </Routes>

      <Modal
        isOpen={isLoginOpen}
        onClose={() => { testhandleLoginSuccess(); setLoginOpen(false); }}
        contentClassName="modal-small"
      >
        <AuthForm onLogin={handleLogin} /> {/* передаємо функцію обробки логіну */}
      </Modal>
    </div>
  );
}

export default AppWrapper;
