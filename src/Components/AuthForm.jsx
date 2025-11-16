import { useState } from 'react';
import './AuthForm.css';

function AuthForm({ type }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  return (
    <div className="auth-form">
      <h2>{type === 'login' ? 'Увійти' : 'Реєстрація'}</h2>
      <form>
        <div className="input-group">
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
        </div>
        <div className="input-group">
          <input
            type="password"
            placeholder="Пароль"
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
        </div>
        {type === 'register' && (
          <div className="input-group">
            <input
              type="password"
              placeholder="Підтвердження пароля"
              value={confirmPassword}
              onChange={e => setConfirmPassword(e.target.value)}
            />
          </div>
        )}
        <button type="submit" className={type === 'login' ? 'btn-login' : 'btn-register'}>
          {type === 'login' ? 'Увійти' : 'Зареєструватися'}
        </button>
      </form>
    </div>
  );
}

export default AuthForm;
