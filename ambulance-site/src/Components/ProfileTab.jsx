import { useState, useEffect } from "react";
import { AuthService } from "../api/services/AuthService.js";
import { PersonApiClient } from "../api/clients/PersonApiClient.js";
import { PersonProfileModel } from "../api/models/PersonModels/PersonProfileModel.js";
import { PersonUpdateModel } from "../api/models/PersonModels/PersonUpdateModel.js";
import { ImageModel } from "../api/models/PersonModels/ImageModel.js";
import PasswordChangeModal from "./PasswordChangeModal.jsx";
import { ChangePasswordRequestModel } from "../api/models/PersonModels/ChangePasswordRequestModel.js";
import "./ProfileTab.css";

function ProfileTab() {
  const [profile, setProfile] = useState(null);
  const [editProfile, setEditProfile] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isEditing, setIsEditing] = useState(false);
  const [saving, setSaving] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false); // для визначення, чи є користувач адміном та далі можливість зміни пароля без старого пароля
  const [showPasswordModal, setShowPasswordModal] = useState(false);
  const [changingPassword, setChangingPassword] = useState(false);

  const genderLabels = {
    // для відображення статі (були пробелми з авто span)
    Man: "Чоловік",
    Woman: "Жінка",
    Other: "Інша стать",
  };

  const personApiClient = new PersonApiClient();

  useEffect(() => {
    loadProfile();
  }, []);

  const loadProfile = async () => {
    try {
      setLoading(true);
      const personId = AuthService.getPersonId();

      if (!personId) {
        throw new Error("Не вдалося отримати ID користувача");
      }

      const profileData = await personApiClient.getPersonProfile(personId);
      const profileModel = new PersonProfileModel(profileData);

      const userData = AuthService.getUserInfo();
      setIsAdmin(userData?.role === "Admin"); // перевіряємо роль користувача
      setProfile(profileModel);
    } catch (err) {
      setError(err.message);
      console.error("Помилка завантаження профілю:", err);
    } finally {
      setLoading(false);
    }
  };

  const handleEdit = () => {
    // Створюємо модель для редагування на основі поточного профілю
    const updateModel = new PersonUpdateModel({
      personId: profile.personId,
      name: profile.name,
      surname: profile.surname,
      middleName: profile.middleName,
      dateOfBirth: profile.dateOfBirth,
      gender: profile.gender,
      phoneNumber: profile.phoneNumber,
      email: profile.email,
      login: profile.login,
      image: profile.image,
    });
    setEditProfile(updateModel);
    setIsEditing(true);
  };

  const handleCancel = () => {
    //очистка всього на всякий
    setIsEditing(false);
    setEditProfile(null);
  };

  const handleSave = async () => {
    try {
      setSaving(true);
      await personApiClient.updatePerson(editProfile);

      // Оновлюємо профіль після успішного збереження
      const updatedProfile = new PersonProfileModel({
        ...profile,
        ...editProfile,
      });
      setProfile(updatedProfile);
      setIsEditing(false);
      setEditProfile(null);
      alert("Профіль успішно оновлено");
    } catch (err) {
      alert("Помилка при оновленні профілю: " + err.message);
      console.error("Помилка збереження:", err);
    } finally {
      setSaving(false);
    }
  };

  const handleChange = (field, value) => {
    // універсальна функція для зміни полів, дещо покращив порівнняно з callform
    setEditProfile((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const handleImageChange = async (event) => {
    const file = event.target.files[0];
    if (file) {
      try {
        const imageModel = await ImageModel.fromFile(file);
        setEditProfile((prev) => ({
          ...prev,
          image: imageModel,
        }));
      } catch (err) {
        console.error("Помилка завантаження зображення:", err);
        alert("Не вдалося завантажити зображення");
      }
    }
  };

  const handlePasswordChange = async (passwordData) => {
    try {
      setChangingPassword(true);
      const personId = AuthService.getPersonId();

      if (isAdmin) {
        // адмін: використовуємо admin-reset-password без перевірки старого паролю
        await personApiClient.adminResetPassword(personId, passwordData.newPassword);
      } else {
        // звичайний користувач: використовуємо change-password зі старим паролем
        const changePasswordModel = new ChangePasswordRequestModel({
          personID: personId,
          oldPassword: passwordData.oldPassword,
          newPassword: passwordData.newPassword,
        });
        await personApiClient.changePassword(changePasswordModel);
      }

      setShowPasswordModal(false);
      alert("Пароль успішно змінено");
    } catch (err) {
      alert("Помилка при зміні паролю: " + err.message);
      console.error("Помилка зміни паролю:", err);
    } finally {
      setChangingPassword(false);
    }
  };

  const currentData = isEditing ? editProfile : profile;

  if (loading)
    return <div className="profile-loading">Завантаження профілю...</div>;
  if (error) return <div className="profile-error">Помилка: {error}</div>;
  if (!currentData)
    return <div className="profile-error">Профіль не знайдено</div>;

  return (
    <div className="profile-tab">
      <div className="profile-content">
        {/* аватарка */}
        <div className="avatar-section">
          <div className="avatar-container">
            {currentData.image && currentData.image.bytes ? (
              <img
                src={currentData.image.toDataUrl()}
                alt="Аватар"
                className="avatar-image"
              />
            ) : (
              <div className="avatar-placeholder">
                {currentData.name.charAt(0)}
                {currentData.surname.charAt(0)}
              </div>
            )}
          </div>
          {isEditing && (
            <div className="avatar-upload">
              <input
                type="file"
                id="avatar-upload"
                accept="image/*"
                onChange={handleImageChange}
                style={{ display: "none" }}
              />
              <label htmlFor="avatar-upload" className="upload-btn">
                Змінити фото
              </label>
            </div>
          )}
        </div>

        <div className="profile-info">
          <div className="info-group">
            <h3>Основна інформація</h3>
            <div className="info-grid">
              <div className="info-item">
                <label>Прізвище:</label>
                {isEditing ? (
                  <input
                    type="text"
                    value={currentData.surname || ""}
                    onChange={(e) => handleChange("surname", e.target.value)}
                    className="edit-input"
                  />
                ) : (
                  <span>{currentData.surname}</span>
                )}
              </div>

              <div className="info-item">
                <label>Ім'я:</label>
                {isEditing ? (
                  <input
                    type="text"
                    value={currentData.name || ""}
                    onChange={(e) => handleChange("name", e.target.value)}
                    className="edit-input"
                  />
                ) : (
                  <span>{currentData.name}</span>
                )}
              </div>

              <div className="info-item">
                <label>По батькові:</label>
                {isEditing ? (
                  <input
                    type="text"
                    value={currentData.middleName || ""}
                    onChange={(e) => handleChange("middleName", e.target.value)}
                    className="edit-input"
                  />
                ) : (
                  <span>{currentData.middleName || ""}</span>
                )}
              </div>

              <div className="info-item">
                <label>Логін:</label>
                {isEditing ? (
                  <input
                    type="text"
                    value={currentData.login || ""}
                    onChange={(e) => handleChange("login", e.target.value)}
                    className="edit-input"
                  />
                ) : (
                  <span>{currentData.login}</span>
                )}
              </div>

              <div className="info-item">
                <label>Дата народження:</label>
                {isEditing ? (
                  <input
                    type="date"
                    value={currentData.dateOfBirth || ""}
                    onChange={(e) =>
                      handleChange("dateOfBirth", e.target.value)
                    }
                    className="edit-input"
                  />
                ) : (
                  <span>
                    {currentData.dateOfBirth
                      ? new Date(currentData.dateOfBirth).toLocaleDateString(
                          "uk-UA"
                        )
                      : ""}
                  </span>
                )}
              </div>

              <div className="info-item">
                <label>Стать:</label>
                {isEditing ? (
                  <select
                    value={currentData.gender ?? ""} // щоб не було uncontrolled to controlled
                    onChange={(e) => handleChange("gender", e.target.value)}
                    className="edit-input"
                  >
                    <option value="Other">{genderLabels["Other"]}</option>
                    <option value="Man">{genderLabels["Man"]}</option>
                    <option value="Woman">{genderLabels["Woman"]}</option>
                    <option value="">Не вказано</option>
                  </select>
                ) : (
                  <span>
                    {genderLabels[currentData.gender] ?? "Не вказано"}
                  </span>
                )}
              </div>
            </div>
          </div>

          <div className="info-group">
            <h3>Контактна інформація</h3>
            <div className="info-grid">
              <div className="info-item">
                <label>Телефон:</label>
                {isEditing ? (
                  <input
                    type="tel"
                    value={currentData.phoneNumber || ""}
                    onChange={(e) =>
                      handleChange("phoneNumber", e.target.value)
                    }
                    className="edit-input"
                  />
                ) : (
                  <span>{currentData.phoneNumber || ""}</span>
                )}
              </div>

              <div className="info-item">
                <label>Email:</label>
                {isEditing ? (
                  <input
                    type="email"
                    value={currentData.email || ""}
                    onChange={(e) => handleChange("email", e.target.value)}
                    className="edit-input"
                  />
                ) : (
                  <span>{currentData.email || ""}</span>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="profile-actions">
        {!isEditing ? (
          <div className="view-actions">
            <button className="edit-profile-btn" onClick={handleEdit}>
              Редагувати профіль
            </button>
            <button
              className="change-password-btn"
              onClick={() => setShowPasswordModal(true)}
            >
              Змінити пароль
            </button>
          </div>
        ) : (
          <div className="edit-actions">
            <button className="save-btn" onClick={handleSave} disabled={saving}>
              {saving ? "Збереження..." : "Зберегти"}
            </button>
            <button
              className="cancel-btn"
              onClick={handleCancel}
              disabled={saving}
            >
              Скасувати
            </button>
          </div>
        )}
      </div>
      {/* нарешті модалка зміни паролю */}
      <PasswordChangeModal
        isOpen={showPasswordModal}
        onClose={() => setShowPasswordModal(false)}
        onSubmit={handlePasswordChange}
        loading={changingPassword}
        isAdmin={isAdmin}
      />
    </div>
  );
}

export default ProfileTab;
