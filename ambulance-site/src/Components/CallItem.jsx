import "./CallItem.css";

const urgencyLevels = [
  {
    value: 1,
    label: "Низька",
    description: "Плановий виклик",
    className: "urgency-low",
  },
  {
    value: 2,
    label: "Середня",
    description: "Терміновий виклик",
    className: "urgency-medium",
  },
  {
    value: 3,
    label: "Висока",
    description: "Екстрений виклик",
    className: "urgency-high",
  },
];

function CallItem({ call, onDelete, showActions = true }) {
  const {
    callId,
    phone,
    patientFullName,
    urgencyType,
    notes,
    startCallTime,
    dispatcherIndentificator,
    hospitalName,
    assignedBrigades,
    estimatedArrival,
  } = call;

  const formattedTime = startCallTime
    ? new Date(startCallTime).toLocaleString()
    : "—";

  // визначаємо рівень терміновості
  const urgency = urgencyLevels.find((u) => u.value === urgencyType) || {
    label: "Невідомо",
    className: "",
  };

  return (
    <div className={`call-item ${urgency.className}`}>
      <div className="item-side">Виклик</div>

      <div className="call-main">
        <div className="call-header">
          <div className="call-title">
            <h4>Виклик #{callId}</h4>
            <span className="call-urgency" title={urgency.description}>
              {urgency.label}
            </span>
          </div>

          {showActions && (
            <div className="call-actions">
              <button className="delete-btn" onClick={() => onDelete?.(callId)}>
                🗑️
              </button>
            </div>
          )}
        </div>

        <div className="call-content">
          <div className="call-patient-info">
            <p>
              <strong>Пацієнт:</strong> {patientFullName ?? "—"}
            </p>
            <p>
              <strong>Телефон:</strong> {phone ?? "—"}
            </p>
            <p>
              <strong>Відправник:</strong> {dispatcherIndentificator ?? "—"}
            </p>
            <p>
              <strong>Лікарня:</strong> {hospitalName ?? "—"}
            </p>
          </div>

          <div className="call-details">
            <p>
              <strong>Примітки:</strong> {notes || "—"}
            </p>
            {estimatedArrival && (
              <p>
                <strong>Орієнтовний час прибуття:</strong> {estimatedArrival}
              </p>
            )}
          </div>

          <div className="call-brigades">
            <strong>Бригади:</strong>
            {assignedBrigades?.length > 0 ? (
              <ul>
                {assignedBrigades.map((b) => (
                  <li key={b.brigadeId}>
                    #{b.brigadeId} — {b.brigadeTypeName}
                  </li>
                ))}
              </ul>
            ) : (
              <p>Не призначено</p>
            )}
          </div>

          <div className="call-footer">
            <span className="call-time">Створено: {formattedTime}</span>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CallItem;
