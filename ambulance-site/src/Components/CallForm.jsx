import { useState, useEffect } from "react";
import { AuthService } from "../api/services/AuthService.js";
import "./CallForm.css";
import CallMap from "./CallMap";
import { CallModel } from "../api/models/CallModel.js";
import { PatientCreateRequestModel } from "../api/models/PersonModels/PatientCreateRequestModel.js";
import { HospitalModel } from "../api/models/HospitalModel.js";

function CallForm({
  onClose,
  onSubmit,
  availableBrigades = [],
  callApiClient,
  callStartTime,
}) {
  const startTime = new Date();
  const [routeDistance, setRouteDistance] = useState(null);
  const [routeDuration, setRouteDuration] = useState(null);
  const [showNewPatientForm, setShowNewPatientForm] = useState(false);
  const [newPatientData, setNewPatientData] = useState({
    name: "",
    surname: "",
    middleName: "",
    phoneNumber: "",
    gender: null,
    dateOfBirth: null,
  });

  const [callData, setCallData] = useState({
    personId: null,
    phone: "",
    urgencyLevelId: 1,
    address: "",
    notes: "",
    selectedBrigadeIds: [],
    hospitalId: null,
  });

  const [addressError, setAddressError] = useState("");
  const [patientsList, setPatientsList] = useState([]);
  const [hospitalsList, setHospitalsList] = useState([]);

  // стан для пошуку пацієнтів
  const [searchQuery, setSearchQuery] = useState("");

  const handleSearchPatients = async (query) => {
    if (!query.trim()) {
      // якщо запит пустий, завантажуємо всіх пацієнтів
      const allPatients = await callApiClient.SearchPatient("");
      setPatientsList(Array.isArray(allPatients) ? allPatients : []);
      return;
    }

    try {
      console.log("Пошук за", query);
      const searchResults = await callApiClient.SearchPatient(query);
      console.log(searchResults);
      setPatientsList(Array.isArray(searchResults) ? searchResults : []);
    } catch (err) {
      console.error("Помилка при пошуку пацієнтів", err);
      setPatientsList([]);
    }
  };

  useEffect(() => {
    // метод дозволяє відкладати виконання коду,
    // пошук- через 300мс після останнього введення
    const timeoutId = setTimeout(() => {
      handleSearchPatients(searchQuery);
    }, 300);
    console.log("searchQuery:", searchQuery);
    console.log("patientsList:", patientsList);

    return () => clearTimeout(timeoutId);
  }, [searchQuery]);

  // метод для скидання вибору пацієнта при оновленні списку
  useEffect(() => {
    setCallData((prev) => ({
      ...prev,
      personId: "",
    }));
  }, [patientsList]);

  // завантажую початкові дані
  useEffect(() => {
    let mounted = true; // флажок компонент живий (щоб не було проблем зі спроьобами оновлення стану після анмаунту)
    (async () => {
      try {
        const p = await callApiClient.SearchPatient("");
        console.log(p);
        const h = await callApiClient.loadHospitals();

        if (!mounted) return; // перевірка чи компонент ще змонтований

        const hospitals = Array.isArray(h)
          ? h.map((hospital) => {
              const hm = new HospitalModel(hospital);
              return hm;
            })
          : [];

        setPatientsList(Array.isArray(p) ? p : []);
        setHospitalsList(hospitals);
      } catch (err) {
        console.error("Помилка при завантаженні пацієнтів/лікарень", err);
      }
    })();
    return () => {
      mounted = false;
    }; // очищення при анмаунті
  }, []);

  useEffect(() => {
    // якщо вибрана бригада не належить новій лікарні, скидаємо її
    if (callData.selectedBrigadeIds.length > 0 && callData.hospitalId) {
      const validBrigadeIds = callData.selectedBrigadeIds.filter(
        (brigadeId) => {
          const brigade = availableBrigades.find((b) => b.id === brigadeId);
          const brigadeMatchesHospital =
            brigade && brigade.hospitalId === parseInt(callData.hospitalId);
          return brigadeMatchesHospital;
        }
      );

      if (validBrigadeIds.length !== callData.selectedBrigadeIds.length) {
        setCallData((prev) => ({
          ...prev,
          selectedBrigadeIds: validBrigadeIds,
        }));
      }
    }
  }, [callData.hospitalId, availableBrigades]);

  const urgencyLevels = [
    {
      value: 1,
      label: "Низька",
      description: "Плановий виклик",
      className: "low",
    },
    {
      value: 2,
      label: "Середня",
      description: "Терміновий виклик",
      className: "medium",
    },
    {
      value: 3,
      label: "Висока",
      description: "Екстрений виклик",
      className: "high",
    },
  ];

  const validateAddress = (address) => {
    if (!address) {
      setAddressError("");
      return true;
    }

    // перевірка формату адреси (місто, вулиця, номер будинку)
    const addressPattern =
      /^(.+),\s*(вул\.|пров\.|просп\.|бульв\.|пл\.)?\s*(.+),\s*(\d+[a-zA-Z]?)$/i;
    if (!addressPattern.test(address)) {
      setAddressError(
        'Будь ласка, введіть повну адресу у форматі: "Місто, вул. Назва, номер"'
      );
      return false;
    }

    setAddressError("");
    return true;
  };

  const getHospitalAddress = () => {
    if (!callData.hospitalId) return null;
    const hospital = hospitalsList.find(
      (h) => h.id === parseInt(callData.hospitalId)
    );
    return hospital ? hospital.address : null;
  };

  const handleInputChange = (field, value) => {
    setCallData((prev) => {
      const newState = {
        ...prev,
        [field]: value,
      };
      return newState;
    });

    // Валідація адреси при зміні
    if (field === "address") {
      validateAddress(value);
    }
  };

  const handlePatientSelect = (personId) => {
    if (personId === "new") {
      setShowNewPatientForm(true);
      setCallData((prev) => ({
        ...prev,
        personId: null,
        phone: "",
      }));
      return;
    }

    setShowNewPatientForm(false);
    const patient = patientsList.find((p) => p.personId === parseInt(personId));
    setCallData((prev) => ({
      ...prev,
      personId: personId,
      phone: patient ? patient.phoneNumber : "нема",
    }));
  };

  const handleNewPatientChange = (field, value) => {
    setNewPatientData((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const handleBrigadeSelect = (brigadeId) => {
    setCallData((prev) => {
      const isSelected = prev.selectedBrigadeIds.includes(brigadeId);
      const newIds = isSelected
        ? prev.selectedBrigadeIds.filter((id) => id !== brigadeId) // видаляємо, якщо вже обрана
        : [...prev.selectedBrigadeIds, brigadeId]; // додаємо, якщо не обрана

      return {
        ...prev,
        selectedBrigadeIds: newIds,
      };
    });
  };

  const handleUrgencySelect = (urgencyLevelId) => {
    setCallData((prev) => ({
      ...prev,
      urgencyLevelId: urgencyLevelId,
    }));
  };

  const handleSubmit = async () => {
    if (callData.selectedBrigadeIds.length === 0) {
      alert("Будь ласка, оберіть щонайменше одну бригаду");
      return;
    }

    if (callData.address && !validateAddress(callData.address)) {
      return;
    }

    try {
      // формування CallModel
      const call = new CallModel({
        dispatcherId: parseInt(AuthService.getPersonId()),
        patientId: callData.personId ? parseInt(callData.personId) : null,
        phone: callData.phone,
        urgencyType: callData.urgencyLevelId,
        address: callData.address,
        notes: callData.notes,
        hospitalId: callData.hospitalId ? parseInt(callData.hospitalId) : null,
        assignedBrigades: callData.selectedBrigadeIds.map((id) => ({
          brigadeId: id,
        })), // всі обрані бригади
        startCallTime: callStartTime,
      });

      console.log(call);
      // формуємо пацієнта, якщо створюється новий
      let personCreateRequest = null;

      if (showNewPatientForm) {
        personCreateRequest = new PatientCreateRequestModel({
          startTime: startTime,
          name: newPatientData.name,
          surname: newPatientData.surname,
          middleName: newPatientData.middleName,
          phoneNumber: newPatientData.phoneNumber || callData.phone,
          gender: newPatientData.gender,
          dateOfBirth: newPatientData.dateOfBirth,
        });
      }

      // порожній дзвінок для callId
      const callId = await callApiClient.createAndFillCall(
        call,
        personCreateRequest
      );

      // повідомляємо батька щоб він завантажив новий виклик та оновив стан
      if (typeof onSubmit === "function") {
        try {
          onSubmit(callId);
        } catch (err) {
          console.warn("onSubmit callback failed:", err);
        }
      }

      onClose();
      alert("Виклик успішно створено");
    } catch (error) {
      alert("Помилка при створенні виклику: " + error.message);
    }
  };

  const availableBrigadesList = availableBrigades.filter((b) => {
    const statusMatch = b.status === "Free";
    const hospitalMatch =
      !callData.hospitalId || b.hospitalId === parseInt(callData.hospitalId);
    const result = statusMatch && hospitalMatch;
    return result;
  });
  const hospitalAddress = getHospitalAddress();

  // Приклади правильних адрес для підказки
  const addressExamples = [
    "Київ, вул. Хрещатик, 25",
    "Львів, вул. Стрийська, 45",
    "Одеса, вул. Дерибасівська, 12",
  ];

  return (
    <div className="call-form">
      <h3>Новий виклик</h3>

      <div className="form-row">
        <div className="form-group">
          <label>Пошук пацієнта:</label>
          <input
            type="text"
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            placeholder="Введіть ім'я, прізвище або телефон..."
            className="search-input"
          />
        </div>

        <div className="form-group">
          <label>Пацієнт:</label>
          <select
            value={callData.personId !== null ? callData.personId : ""}
            onChange={(e) => handlePatientSelect(e.target.value)}
          >
            <option value="">
              {patientsList.length === 0
                ? "Немає результатів"
                : "Виберіть пацієнта"}
            </option>
            <option value="new">+ Нова людина</option>
            {patientsList.map((patient) => (
              <option key={patient.personId} value={patient.personId}>
                {patient.surname} {patient.name} {patient.middleName}{" "}
                {patient.phoneNumber}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group">
          <label>Номер телефону:</label>
          <input
            type="tel"
            value={callData.phone}
            onChange={(e) => handleInputChange("phone", e.target.value)}
            placeholder="+380501112233"
          />
        </div>
      </div>

      {showNewPatientForm && ( // форма для нового пацієнта
        <div
          className="form-group full-width"
          style={{
            border: "1px solid #ccc",
            padding: "10px",
            borderRadius: "4px",
            marginBottom: "15px",
          }}
        >
          <h4 style={{ marginTop: 0 }}>Дані нової людини</h4>
          <div className="form-row">
            <div className="form-group">
              <label>Ім'я:</label>
              <input
                type="text"
                value={newPatientData.name}
                onChange={(e) => handleNewPatientChange("name", e.target.value)}
                placeholder="Ім'я"
              />
            </div>
            <div className="form-group">
              <label>Прізвище:</label>
              <input
                type="text"
                value={newPatientData.surname}
                onChange={(e) =>
                  handleNewPatientChange("surname", e.target.value)
                }
                placeholder="Прізвище"
              />
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>По батькові:</label>
              <input
                type="text"
                value={newPatientData.middleName}
                onChange={(e) =>
                  handleNewPatientChange("middleName", e.target.value)
                }
                placeholder="По батькові (опціонально)"
              />
            </div>
            <div className="form-group">
              <label>Стать:</label>
              <select
                value={newPatientData.gender ?? ""}
                onChange={(e) =>
                  handleNewPatientChange("gender", e.target.value || null)
                }
              >
                <option value="Other">Інша стать</option>
                <option value="Man">Чоловік</option>
                <option value="Woman">Жінка</option>
                <option value="">Не вказано</option>
              </select>
            </div>
          </div>
          <div className="form-row">
            <div className="form-group">
              <label>Дата народження:</label>
              <input
                type="date"
                value={newPatientData.dateOfBirth}
                onChange={(e) =>
                  handleNewPatientChange("dateOfBirth", e.target.value)
                }
              />
            </div>
          </div>
        </div>
      )}

      <div className="form-group full-width">
        <label>Рівень терміновості:</label>
        <div className="urgency-levels">
          {urgencyLevels.map((level) => (
            <div
              key={level.value}
              className={`urgency-option urgency-${level.className} ${
                callData.urgencyLevelId === level.value ? "selected" : ""
              }`}
              onClick={() => handleUrgencySelect(level.value)}
            >
              <div>{level.label}</div>
              <div style={{ fontSize: "0.8rem", color: "#6b7280" }}>
                {level.description}
              </div>
            </div>
          ))}
        </div>
      </div>

      <div className="form-group full-width">
        <label>Адреса виклику:</label>
        <input
          type="text"
          value={callData.address}
          onChange={(e) => handleInputChange("address", e.target.value)}
          className={`full-input ${addressError ? "input-error" : ""}`}
        />
        {addressError && <div className="error-message">{addressError}</div>}
        <div className="address-hint">
          <strong>Приклади правильного формату:</strong>
          <ul>
            {addressExamples.map((example, index) => (
              <li key={index}>{example}</li>
            ))}
          </ul>
        </div>
      </div>

      <div className="form-group full-width">
        <label>Лікарня:</label>
        <select
          value={callData.hospitalId || ""}
          onChange={(e) =>
            handleInputChange(
              "hospitalId",
              e.target.value ? parseInt(e.target.value) : null
            )
          }
          className="full-input"
        >
          <option value="">Не призначати лікарню</option>
          {hospitalsList.length > 0 ? (
            hospitalsList.map((hospital) => (
              <option key={hospital.id} value={hospital.id}>
                {hospital.name}
              </option>
            ))
          ) : (
            <option disabled>Немає доступних лікарень</option>
          )}
        </select>
      </div>

      <div className="form-group full-width">
        <label>Примітки:</label>
        <textarea
          value={callData.notes}
          onChange={(e) => handleInputChange("notes", e.target.value)}
          placeholder="Додаткові примітки щодо виклику"
          rows="3"
          className="full-textarea"
        />
      </div>

      <div className="brigades-selection">
        <h4>Оберіть бригади (можна декілька)</h4>
        <div className="brigades-list">
          {availableBrigadesList.length > 0 ? (
            availableBrigadesList.map((brigade) => {
              const isSelected = callData.selectedBrigadeIds.includes(
                brigade.id
              );
              return (
                <div
                  key={brigade.id}
                  className={`brigade-option ${isSelected ? "selected" : ""}`}
                  onClick={() => handleBrigadeSelect(brigade.id)}
                >
                  <div className="brigade-checkbox">
                    <input
                      type="checkbox"
                      checked={isSelected}
                      onChange={(e) => {
                        e.stopPropagation();
                        handleBrigadeSelect(brigade.id);
                      }}
                      style={{ cursor: "pointer" }}
                    />
                  </div>
                  <div className="brigade-info">
                    <div className="brigade-type">
                      Бригада #{brigade.id} - {brigade.type}
                    </div>
                    <div className="brigade-members">
                      {brigade.members.join(", ")}
                    </div>
                    <span
                      className={`brigade-status status-${
                        brigade.status === "Вільна" ? "available" : "busy"
                      }`}
                    >
                      {brigade.status}
                    </span>
                  </div>
                </div>
              );
            })
          ) : (
            <div
              style={{
                gridColumn: "1 / -1",
                textAlign: "center",
                color: "#6b7280",
              }}
            >
              Немає вільних бригад
            </div>
          )}
        </div>
      </div>

      <div className="map-placeholder">
        {callData.address && hospitalAddress ? (
          <CallMap
            startAddress={callData.address}
            endAddress={hospitalAddress}
            onRouteFound={({ distance, duration }) => {
              setRouteDistance(distance);
              setRouteDuration(duration);
            }}
          />
        ) : (
          <div className="map-placeholder-text">
            {!callData.address && !hospitalAddress ? (
              <p>
                Введіть адресу виклику та оберіть лікарню для відображення мапи
              </p>
            ) : !callData.address ? (
              <p>Введіть адресу виклику для відображення мапи</p>
            ) : (
              <p>Оберіть лікарню для відображення мапи</p>
            )}
          </div>
        )}
      </div>

      <div className="distribution-info">
        <div className="info-row">
          <span>Орієнтовний час прибуття:</span>
          <span className="time-value">
            {routeDuration ? Math.round(routeDuration / 60) + " хв" : "~ ..."}
          </span>
        </div>

        <div className="info-row">
          <span>Відстань до лікарні:</span>
          <span className="distance-value">
            {routeDistance
              ? (routeDistance / 1000).toFixed(1) + " км"
              : "~ ..."}
          </span>
        </div>
      </div>

      <div className="form-actions">
        <button type="button" className="cancel-btn" onClick={onClose}>
          Скасувати
        </button>
        <button
          type="button"
          className="submit-call"
          onClick={handleSubmit}
          disabled={
            !callData.phone ||
            callData.selectedBrigadeIds.length === 0 ||
            !!addressError
          }
        >
          Створити виклик
        </button>
      </div>
    </div>
  );
}

export default CallForm;
