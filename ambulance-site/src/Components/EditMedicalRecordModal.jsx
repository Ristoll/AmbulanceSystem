import { useState } from "react";
import "./EditMedicalRecordModal.css";

export default function EditMedicalRecordModal({ record, medicalCardId, onSave, onClose }) {
  const [form, setForm] = useState({
    medicalRecordId: record?.medicalRecordId || 0,
    medicalCardId: medicalCardId,
    diagnoses: record?.diagnoses || "",
    symptoms: record?.symptoms || "",
    treatment: record?.treatment || "",
    photoUrl: record?.photoUrl || ""
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSave = () => {
    onSave(form);
  };

  return (
    <div className="modal-overlay">
      <div className="modal">
        <h2>{record ? "Редагувати запис" : "Новий запис"}</h2>

        <label>Діагнози:</label>
        <textarea name="diagnoses" value={form.diagnoses} onChange={handleChange} />

        <label>Симптоми:</label>
        <textarea name="symptoms" value={form.symptoms} onChange={handleChange} />

        <label>Лікування:</label>
        <textarea name="treatment" value={form.treatment} onChange={handleChange} />

        <label>Фото з місця події / травми (URL):</label>
        <input name="photoUrl" value={form.photoUrl} onChange={handleChange} />

        <div className="modal-buttons">
          <button onClick={handleSave}>Зберегти</button>
          <button className="close-btn" onClick={onClose}>Закрити</button>
        </div>
      </div>
    </div>
  );
}
