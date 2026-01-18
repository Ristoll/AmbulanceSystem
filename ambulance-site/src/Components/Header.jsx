import './Header.css';

function Header({ onLoginOpen, onRegisterOpen }) {
  return (
    <header className="header">
      <div className="logo">🚑 Швидка допомога України</div>

      <div className="auth-buttons">
        <button className="head-btn-login" onClick={onLoginOpen}>Увійти</button>
      </div>
    </header>
  );
}

export default Header;