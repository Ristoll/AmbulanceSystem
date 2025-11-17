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
import { PersonApiClient } from "./public/apiclients/PersonApiClient.js";
import { AuthService } from "./public/apiclients/AuthService.js";
import { LoginRequestModel, AuthResponseModel } from "./public/models/AuthModels.js";

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

  // функція для переходу після успішного логіну
  const handleLoginSuccess = (userRole) => {
    switch(userRole) {
        case "Admin":
            navigate('/admin');
            break;
        case "Dispatcher":
            navigate('/dispatcher');
            break;
        case "Patient":
            navigate('/patient');
            break;
        default:
            navigate('/start');
    }
  };

  // функція для обробки логіну
  const handleLogin = async (login, password) => {
    try {
      const loginRequest = new LoginRequestModel({ login, password }); // створюємо модель для API
      const authResponseData = await apiClient.authenticate(loginRequest); // виклик API

      if (!authResponseData) {
        alert("Невірний логін або пароль"); // якщо API повернув null
        return;
      }

      const authResponse = new AuthResponseModel(authResponseData); // обгортаємо відповідь у модель

      // зберігаємо токен і інформацію про юзера у браузері
      AuthService.saveToken(authResponse.jwtToken);
      AuthService.saveUserInfo({
            login: authResponse.login,
            userRole: authResponse.userRole});
            
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
        
        <Route path="*" element={<Navigate to="/start" replace />} />
      </Routes>

      {/* Модальні вікна */}
      <Modal 
        isOpen={isLoginOpen} 
        onClose={() => { handleLoginSuccess(); setLoginOpen(false); }} 
        contentClassName="modal-small"
      >
        <AuthForm onLogin={handleLogin} /> {/* передаємо функцію обробки логіну */}
      </Modal>
    </div>
  );
}

export default AppWrapper;
