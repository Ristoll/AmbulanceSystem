import { useEffect, useState } from 'react';
import { CallApiClient } from '../api/clients/CallApiClient';
import { AuthService } from '../api/services/AuthService';
import './CallsPanel.css';

function CallsPanel({ apiBaseUrl }) {
  const [calls, setCalls] = useState([]);
  const [loading, setLoading] = useState(true);

  // Ініціалізація API-клієнта з токеном
  const apiClient = new CallApiClient(apiBaseUrl, () => AuthService.getToken());

  useEffect(() => {
    const fetchCalls = async () => {
      setLoading(true);
      try {
        const data = await apiClient.loadCalls();
        if (data) setCalls(data);
      } catch (err) {
        console.error('Помилка завантаження дзвінків:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchCalls();
  }, [apiBaseUrl]);

  if (loading) return <p>Завантаження дзвінків...</p>;
  if (!calls.length) return <p>Немає дзвінків у базі даних</p>;

  return (
    <div className="calls-panel">
      <h3>Список викликів</h3>
      <table className="calls-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Пацієнт</th>
            <th>Диспетчер</th>
            <th>Бригада</th>
            <th>Телефон</th>
            <th>Адреса</th>
            <th>Статус</th>
            <th>Дата початку</th>
          </tr>
        </thead>
        <tbody>
          {calls.map(call => (
            <tr key={call.callId}>
              <td>{call.callId}</td>
              <td>{call.patientName || call.patientId}</td>
              <td>{call.dispatcherName || call.dispatcherId}</td>
              <td>{call.assignedBrigades?.map(b => b.name).join(', ') || '-'}</td>
              <td>{call.phone || '-'}</td>
              <td>{call.address || '-'}</td>
              <td>{call.callStatusId || 'Новий'}</td>
              <td>{call.startCallTime ? new Date(call.startCallTime).toLocaleString() : '-'}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CallsPanel;
