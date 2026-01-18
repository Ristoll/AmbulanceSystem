import { useState, useEffect } from 'react';
import { BarChart, Bar, XAxis, YAxis, Tooltip, CartesianGrid, ResponsiveContainer, Legend, LabelList } from 'recharts';
import { AnaliticsApiClient } from '../api/clients/AnaliticsApiClient.js';
import './AnalyticsPanel.css';

function AnalyticsPanel({ tokenProvider }) {
  const apiClient = new AnaliticsApiClient('http://localhost:7090', tokenProvider);

  const [callData, setCallData] = useState([]);
  const [resourceData, setResourceData] = useState([]);
  const [diseaseData, setDiseaseData] = useState([]);
  const [allergyData, setAllergyData] = useState([]);

  useEffect(() => {
  const loadAnalytics = async () => {
    try {
      const calls = await apiClient.getCallAnalytics();
      console.log("Запит ресурсів бригад...");
const resources = await apiClient.getBrigadeResourceAnalytics();
console.log("Отримані ресурси:", resources);
      const diseases = await apiClient.getDeceaseAnalytics();
      const allergies = await apiClient.getAllergyAnalytics();

      // Дзвінки — об’єкт { "2025-11-25T00:00:00": 3, ... }
      const callArray = Object.entries(calls).map(([date, count]) => ({
        date: new Date(date).toLocaleDateString(),
        count
      }));

      // Ресурси — об’єкт { "Плазма": 10, "Шприци": 25, ... }
      const resourceArray = Object.entries(resources).map(([name, count]) => ({
        name,
        count
      }));

      // Хвороби — об’єкт { "Діабет": 5, ... }
      const diseaseArray = Object.entries(diseases).map(([name, count]) => ({
        name,
        count
      }));

      // Алергії — об’єкт { "Пилок": 3, ... }
      const allergyArray = Object.entries(allergies).map(([name, count]) => ({
        name,
        count
      }));

      // Встановлюємо стани
      setCallData(callArray);
      setResourceData(resourceArray);
      setDiseaseData(diseaseArray);
      setAllergyData(allergyArray);

    } catch (err) {
      console.error("Помилка при завантаженні аналітики", err);
    }
  };

  loadAnalytics();
}, [tokenProvider]);


  const renderChart = (data, dataKey, nameKey, title, color = "#2563eb") => (
  <div className="chart-card">
    <h3>{title}</h3>
    <ResponsiveContainer width="100%" height={400}> {/* збільшена висота */}
      <BarChart data={data} margin={{ top: 20, right: 20, left: 20, bottom: 5 }}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis 
          dataKey={nameKey} 
          angle={-30} 
          textAnchor="end" 
          interval={0} 
          tick={{ fontSize: 14 }} 
        />
        <YAxis tick={{ fontSize: 14 }} />
        <Tooltip formatter={(value) => [value, "Кількість"]} />
        <Bar dataKey={dataKey} fill={color} radius={[6, 6, 0, 0]}>
          <LabelList dataKey={dataKey} position="top" />
        </Bar>
      </BarChart>
    </ResponsiveContainer>
  </div>
);


  return (
    <div className="analytics-panel">
      <div className="charts-row">
        {renderChart(callData, 'count', 'date', 'Кількість дзвінків за тиждень', "#2563eb")}
        {renderChart(resourceData, 'count', 'name', 'Використані медикаменти', "#10b981")}
      </div>
      <div className="charts-row">
        {renderChart(diseaseData, 'count', 'name', 'Хронічні захворювання', "#f59e0b")}
        {renderChart(allergyData, 'count', 'name', 'Алергії', "#ef4444")}
      </div>
    </div>
  );
}

export default AnalyticsPanel;
