import { useState } from "react";
import "./AuthForm.css";

function AuthForm({ onLogin }) {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault(); // щоб пусті не пускало при перезавантаженні сторінки (як в гайді написано - тоді відправляється форма браузером за замовченням)
    if (onLogin) {
      onLogin(login, password);
    }
  };

  return (
    <div className="auth-form">
      <h2>Увійти</h2>
      <form onSubmit={handleSubmit}>
        {" "}
        {/*стандартна механіка для полів вводу*/}
        <div className="input-group">
          <input
            type="text"
            value={login}
            onChange={(e) => setLogin(e.target.value)}
            placeholder="Логін"
          />
        </div>
        <div className="input-group">
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Пароль"
          />
        </div>
        <button type="submit" className="btn-login">
          Увійти
        </button>
      </form>
    </div>
  );
}

export default AuthForm;
