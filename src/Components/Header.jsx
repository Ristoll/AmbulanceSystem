import './Header.css';
import React from 'react';

function Header({ onLoginOpen, onRegisterOpen }) {
  return (
    <header className="header">
      <div className="logo">🚑 Швидка допомога України</div>

      <nav className="nav">
        <a href="#">Головна</a>
        <a href="#">Контакти</a>
        <a href="#">Про нас</a>
      </nav>

      <div className="auth-buttons">
        <button className="head-btn-login" onClick={onLoginOpen}>Увійти</button>
        <button className="head-btn-register" onClick={onRegisterOpen}>Реєстрація</button>
      </div>
    </header>
  );
}

export default Header;