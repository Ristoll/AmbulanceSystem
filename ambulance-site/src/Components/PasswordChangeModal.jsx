import { useState } from "react";
import Modal from "./Modal";
import "./PasswordChangeModal.css";

function PasswordChangeModal({
  isOpen,
  onClose,
  onSubmit,
  loading,
  isAdmin = false,
}) {
  const [formData, setFormData] = useState({
    oldPassword: "",
    newPassword: "",
    confirmPassword: "",
  });
  const [errors, setErrors] = useState({});

  const handleChange = (field, value) => {
    setFormData((prev) => ({
      ...prev,
      [field]: value,
    }));

    // очищаємо помилку при зміні поля
    if (errors[field]) {
      setErrors((prev) => ({
        ...prev,
        [field]: "",
      }));
    }
  };

  const validateForm = () => {
    const newErrors = {};

    // для не-адмінів перевіряємо старий пароль
    if (!isAdmin && !formData.oldPassword) {
      newErrors.oldPassword = "Введіть поточний пароль";
    }

    if (!formData.newPassword) {
      newErrors.newPassword = "Введіть новий пароль";
    } else if (formData.newPassword.length < 8) {
      newErrors.newPassword = "Пароль має бути не менше 6 символів";
    }

    if (!formData.confirmPassword) {
      newErrors.confirmPassword = "Підтвердіть новий пароль";
    } else if (formData.newPassword !== formData.confirmPassword) {
      newErrors.confirmPassword = "Паролі не співпадають";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (validateForm()) {
      onSubmit({
        oldPassword: isAdmin ? "" : formData.oldPassword, // для адміна старий пароль не потрібен
        newPassword: formData.newPassword,
      });
    }
  };

  const handleClose = () => {
    // скидаємо форму при закритті на всякий
    setFormData({
      oldPassword: "",
      newPassword: "",
      confirmPassword: "",
    });
    setErrors({});
    onClose();
  };

  return (
    <Modal
      isOpen={isOpen}
      onClose={handleClose}
      contentClassName="password-modal"
    >
      <div className="password-modal-header">
        <h3>Зміна паролю</h3>
      </div>

      <form onSubmit={handleSubmit} className="password-form">
        {!isAdmin && (
          <div className="form-group">
            <label>Поточний пароль:</label>
            <input
              type="password"
              value={formData.oldPassword}
              onChange={(e) => handleChange("oldPassword", e.target.value)}
              className={errors.oldPassword ? "error" : ""}
              disabled={loading}
              placeholder="Введіть поточний пароль"
            />
            {errors.oldPassword && (
              <span className="error-text">{errors.oldPassword}</span>
            )}
          </div>
        )}

        {isAdmin && (
          <div className="admin-notice">
            <p>
              Ви змінюєте пароль як адміністратор. Старий пароль не потрібен.
            </p>
          </div>
        )}

        <div className="form-group">
          <label>Новий пароль:</label>
          <input
            type="password"
            value={formData.newPassword}
            onChange={(e) => handleChange("newPassword", e.target.value)}
            className={errors.newPassword ? "error" : ""}
            disabled={loading}
            placeholder="Введіть новий пароль"
          />
          {errors.newPassword && (
            <span className="error-text">{errors.newPassword}</span>
          )}
        </div>

        <div className="form-group">
          <label>Підтвердження паролю:</label>
          <input
            type="password"
            value={formData.confirmPassword}
            onChange={(e) => handleChange("confirmPassword", e.target.value)}
            className={errors.confirmPassword ? "error" : ""}
            disabled={loading}
            placeholder="Повторіть новий пароль"
          />
          {errors.confirmPassword && (
            <span className="error-text">{errors.confirmPassword}</span>
          )}
        </div>

        <div className="password-requirements">
          <p>Вимоги до паролю:</p>
          <ul>
            <li>Мінімум 8 символів</li>
            {isAdmin && <li>Рекомендується використовувати складний пароль</li>}
          </ul>
        </div>

        <div className="modal-actions">
          <button type="submit" className="save-btn" disabled={loading}>
            {loading ? "Зміна..." : "Змінити пароль"}
          </button>
          <button
            type="button"
            className="cancel-btn"
            onClick={handleClose}
            disabled={loading}
          >
            Скасувати
          </button>
        </div>
      </form>
    </Modal>
  );
}

export default PasswordChangeModal;
