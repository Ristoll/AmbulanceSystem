import { useEffect, useState } from "react";
import "./PatientCardsPanel.css";

import EditMedicalCardModal from "./EditMedicalCardModal";
import EditMedicalRecordModal from "./EditMedicalRecordModal";

import { MedicalCardApiClient } from "../api/clients/MedicalCardApiClient";

function PatientCardsPanel({ apiBaseUrl, tokenProvider }) {
  const api = new MedicalCardApiClient(apiBaseUrl, tokenProvider);

  const [cards, setCards] = useState([]);
  const [expandedCardId, setExpandedCardId] = useState(null);

  // Модалки
  const [editingCard, setEditingCard] = useState(null);
  const [editingRecord, setEditingRecord] = useState(null);
  const [currentCardId, setCurrentCardId] = useState(null);

  // =============================
  //     LOAD CARDS
  // =============================
  const refreshCards = async () => {
    const data = await api.loadAllMedicalCards();
    if (data) setCards(data);
  };

  useEffect(() => {
    refreshCards();
  }, []);

  // =============================
  //     UPDATE CARD
  // =============================
  const saveCard = async (cardDto) => {
    await api.updateMedicalCard(cardDto);
    setEditingCard(null);
    refreshCards();
  };

  // =============================
  //     UPDATE OR CREATE RECORD
  // =============================
  const saveRecord = async (recordDto) => {
    if (recordDto.medicalRecordId === 0) {
      await api.createMedicalRecord(recordDto);
    } else {
      await api.updateMedicalRecord(recordDto);
    }

    setEditingRecord(null);
    refreshCards();
  };

  // =============================
  //     UI Handlers
  // =============================
  const toggleCard = (id) => {
    setExpandedCardId(expandedCardId === id ? null : id);
  };

  return (
    <div className="patient-cards-panel">

      {cards.map((card) => (
        <div key={card.medicalCardId} className="card patient-card">

          {/* HEADER */}
          <div className="card-header" onClick={() => toggleCard(card.medicalCardId)}>
            <h3>Картка пацієнта: {card.medicalCardPatientName}</h3>

            <div className="header-actions">
              <button
                onClick={(e) => {
                  e.stopPropagation();
                  setEditingCard(card);
                }}
              >
                Редагувати
              </button>

              <button
                onClick={(e) => {
                  e.stopPropagation();
                  setCurrentCardId(card.medicalCardId);
                  setEditingRecord({
                    medicalRecordId: 0,
                    medicalCardId: card.medicalCardId,
                    diagnoses: "",
                    symptoms: "",
                    treatment: "",
                    photoUrl: ""
                  });
                }}
              >
                + Новий запис
              </button>
            </div>
          </div>

          {/* BODY */}
          {expandedCardId === card.medicalCardId && (
            <div className="card-body">

              <p><strong>Група крові:</strong> {card.bloodType || "-"}</p>
              <p><strong>Зріст:</strong> {card.height || "-"} см</p>
              <p><strong>Вага:</strong> {card.weight || "-"} кг</p>
              <p><strong>Нотатки:</strong> {card.notes || "-"}</p>

              <div className="allergies">
                <strong>Алергії:</strong> {card.allergies?.join(", ") || "-"}
              </div>

              <div className="chronic">
                <strong>Хронічні захворювання:</strong> {card.chronicDiseases?.join(", ") || "-"}
              </div>

              {/* Medical Records */}
              <div className="medical-records">
                <h4>Медичні записи:</h4>

                {card.medicalRecords.length === 0 && (
                  <p className="empty">Записів немає</p>
                )}

                {card.medicalRecords.map((record) => (
                  <div key={record.medicalRecordId} className="record">

                    <div className="record-header">
                      <h4>
                        Запис від{" "}
                        {record.dataTime
                          ? new Date(record.dataTime).toLocaleDateString()
                          : "-"}
                      </h4>

                      <button
                        onClick={() => {
                          setCurrentCardId(card.medicalCardId);
                          setEditingRecord(record);
                        }}
                      >
                        Редагувати
                      </button>
                    </div>

                    <p><strong>Діагнози:</strong> {record.diagnoses || "-"}</p>
                    <p><strong>Симптоми:</strong> {record.symptoms || "-"}</p>
                    <p><strong>Лікування:</strong> {record.treatment || "-"}</p>

                    {record.photoUrl && (
                      <img
                        src={record.photoUrl}
                        alt="record"
                        className="record-photo"
                      />
                    )}
                  </div>
                ))}
              </div>
            </div>
          )}

        </div>
      ))}

      {/* MODALS */}

      {editingCard && (
        <EditMedicalCardModal
          card={editingCard}
          onSave={saveCard}
          onClose={() => setEditingCard(null)}
        />
      )}

      {editingRecord && (
        <EditMedicalRecordModal
          record={editingRecord.medicalRecordId === 0 ? null : editingRecord}
          medicalCardId={currentCardId}
          onSave={saveRecord}
          onClose={() => setEditingRecord(null)}
        />
      )}
    </div>
  );
}

export default PatientCardsPanel;
