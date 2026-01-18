import { useEffect, useState } from "react";
import { BrigadeApiClient } from "../api/clients/BrigadeApiClient.js";
import ItemApiClient from "../api/clients/ItemApiClient.js";
import MedicineCard from "./MedicineCard";
import "./MedicinesPanel.css";

function MedicinesPanel({ apiBaseUrl, tokenProvider }) {
  const [brigades, setBrigades] = useState([]);
  const [loading, setLoading] = useState(true);

  // Ініціалізуємо API-клієнти
  const brigadeApi = new BrigadeApiClient(apiBaseUrl, tokenProvider);
  const itemApi = new ItemApiClient(apiBaseUrl, tokenProvider);

  useEffect(() => {
  const load = async () => {
    setLoading(true);

    try {
      // 1. Завантажуємо всі бригади
      const allBrigades = await brigadeApi.getAllBrigades();
      console.log("Всі бригади з API:", allBrigades);

      if (!allBrigades) {
        setBrigades([]);
        return;
      }

      // Підготовка структури
      const mapped = allBrigades.map(b => ({
        brigadeId: b.brigadeId,
        brigadeName: b.brigadeName,
        items: []
      }));

      // 2. Для кожної бригади завантажуємо медикаменти
      for (let i = 0; i < mapped.length; i++) {
        try {
          const items = await itemApi.loadBrigadeItems(mapped[i].brigadeId);
          console.log(`Медикаменти бригади ${mapped[i].brigadeId}:`, items);
          mapped[i].items = items || [];
        } catch (err) {
          console.error(`Помилка завантаження предметів бригади ${mapped[i].brigadeId}`, err);
        }
      }

      console.log("Підготовлені бригади з медикаментами:", mapped);

      setBrigades(mapped);
    } catch (err) {
      console.error("Помилка при завантаженні бригад:", err);
    } finally {
      setLoading(false);
    }
  };

  load();
}, [apiBaseUrl]);


  if (loading) return <p>Завантаження бригад та медикаментів...</p>;
  if (!brigades.length) return <p>Бригади не знайдені</p>;

  return (
    <div className="medicines-panel">
      <h2 className="medicines-title">Медикаменти по бригадах</h2>

      {brigades.map(brigade => (
        <div key={brigade.brigadeId} className="brigade-section">
          <h3 className="brigade-header">
            {brigade.brigadeName || `Бригада №${brigade.brigadeId}`}
          </h3>

          <div className="medicines-grid">
            {brigade.items.length > 0 ? (
              brigade.items.map(m => (
                <MedicineCard
                  key={m.itemId}
                  id={m.itemId}
                  name={m.itemName}
                  itemType={m.itemType}
                  unitType={m.unitType}
                  quantity={m.quantity}
                  expiryDate={m.expiryDate}
                  imageUrl={m.imageUrl}
                />
              ))
            ) : (
              <p className="no-items">Немає медикаментів</p>
            )}
          </div>
        </div>
      ))}
    </div>
  );
}

export default MedicinesPanel;
