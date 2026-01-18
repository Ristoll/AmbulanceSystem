import { useState } from "react";
import "./EditMedicalCardModal.css";

export default function EditMedicalCardModal({ card, onSave, onClose }) {
  const [form, setForm] = useState({
    medicalCardId: card.medicalCardId,
    bloodType: card.bloodType || "",
    height: card.height || "",
    weight: card.weight || "",
    notes: card.notes || "",
    allergies: card.allergies?.join(", ") || "",
    chronicDiseases: card.chronicDiseases?.join(", ") || "",
    photoUrl: card.photoUrl || ""
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSave = () => {
    const dto = {
      ...form,
      allergies: form.allergies.split(",").map(a => a.trim()),
      chronicDiseases: form.chronicDiseases.split(",").map(c => c.trim())
    };
    onSave(dto);
  };

  return (
    <div className="modal-overlay">
      <div className="modal">
        <h2>Редагування картки</h2>

        <label>Група крові:</label>
        <input name="bloodType" value={form.bloodType} onChange={handleChange} />

        <label>Зріст (см):</label>
        <input name="height" type="number" value={form.height} onChange={handleChange} />

        <label>Вага (кг):</label>
        <input name="weight" type="number" value={form.weight} onChange={handleChange} />

        <label>Нотатки:</label>
        <textarea name="notes" value={form.notes} onChange={handleChange} />

        <label>Алергії (через кому):</label>
        <input name="allergies" value={form.allergies} onChange={handleChange} />

        <label>Хронічні хвороби (через кому):</label>
        <input name="chronicDiseases" value={form.chronicDiseases} onChange={handleChange} />

        <label>Фото (URL):</label>
        <input name="photoUrl" value={form.photoUrl} onChange={handleChange} />

        <div className="modal-buttons">
          <button onClick={handleSave}>Зберегти</button>
          <button className="close-btn" onClick={onClose}>Закрити</button>
        </div>
      </div>
    </div>
  );
}
