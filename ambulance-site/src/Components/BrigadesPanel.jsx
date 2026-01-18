// BrigadesPanel.jsx
import './BrigadesPanel.css';
import { useEffect, useState } from 'react';
import BrigadeCard from './BrigadeCard';
import { BrigadeApiClient } from '../api/clients/BrigadeApiClient';

function BrigadesPanel({ apiBaseUrl, tokenProvider }) {
  const [brigades, setBrigades] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const apiClient = new BrigadeApiClient(apiBaseUrl, tokenProvider);

  const normalizeBrigade = async (b) => {
    let members = [];
    try {
      members = await apiClient.getBrigadeMembers(b.brigadeId);
      console.log(`Brigade ${b.brigadeId} members API response:`, members);
    } catch (err) {
      console.error(`Не вдалося завантажити членів бригади ${b.brigadeId}:`, err);
    }

    // Перевіряємо, чи масив, і правильно беремо поля з великої літери
    const memberNames = Array.isArray(members)
      ? members.map((m, index) => {
          const name = m.personFullName?.trim() || `Учасник №${index + 1}`;
          const role = m.roleName || 'Без ролі';
          const spec = m.specializationTypeName || 'Без спеціалізації';
          return `${name} (${role}, ${spec})`;
        })
      : ["Немає членів бригади"];

    return {
      id: b.brigadeId,
      status: b.brigadeStateCode,
      type: b.brigadeTypeName,
      members: memberNames,
      hospitalId: b.hospitalId ?? b.HospitalId ?? null
    };
  };

  useEffect(() => {
    const fetchBrigades = async () => {
      setLoading(true);
      setError(null);

      try {
        const data = await apiClient.getAllBrigades();
        if (!Array.isArray(data)) {
          setBrigades([]);
          return;
        }

        // Паралельно підвантажуємо членів бригад
        const normalized = await Promise.all(data.map(normalizeBrigade));
        setBrigades(normalized);
      } catch (err) {
        console.error("Помилка завантаження бригад:", err);
        setError(err.message || "Невідома помилка при завантаженні бригад");
      } finally {
        setLoading(false);
      }
    };

    fetchBrigades();
  }, [apiBaseUrl, tokenProvider]);

  if (loading) return <p>Завантаження бригад...</p>;
  if (error) return <p style={{ color: 'red' }}>Помилка: {error}</p>;
  if (brigades.length === 0) return <p>Бригад не знайдено.</p>;

  return (
    <div className="brigades-container">
      {brigades.map(b => (
        <BrigadeCard
          key={b.id}
          id={b.id}
          status={b.status}
          type={b.type}
          members={b.members} // Тепер буде "Ім'я (роль, спеціалізація)"
          onSelect={() => console.log("Вибрана бригада:", b.id)}
        />
      ))}
    </div>
  );
}

export default BrigadesPanel;
