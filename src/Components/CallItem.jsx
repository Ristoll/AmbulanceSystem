import './CallItem.css';

function CallItem({ call, onEdit, onDelete, showActions = true }) {
  const { 
    id, 
    phone, 
    patient, 
    name, 
    surname, 
    middlename, 
    age, 
    specification, 
    description,
    createdAt,
    status 
  } = call;

  return (
    <div className="call-item">
      <div className="item-side">Виклик</div>
      <div className="call-main">
          <div className="call-header">
          <div className="call-title">
            <h4>Виклик #{id}</h4>
            <span className={`call-status ${status === 'Новий' ? 'status-new' : 'status-processed'}`}>
              {status}
            </span>
          </div>
          {showActions && (
            <div className="call-actions">
              <button className="edit-btn" onClick={() => onEdit?.(id)}>✏️</button>
              <button className="delete-btn" onClick={() => onDelete?.(id)}>🗑️</button>
            </div>
          )}
        </div>

        <div className="call-content">
          <div className="call-patient-info">
            <p><strong>Пацієнт:</strong> {patient || `${surname} ${name} ${middlename}`}</p>
            <p><strong>Телефон:</strong> {phone}</p>
            <p><strong>Вік:</strong> {age} років</p>
          </div>

          <div className="call-details">
            <p><strong>Специфікація:</strong> {specification}</p>
            <p><strong>Опис:</strong> {description}</p>
          </div>

          <div className="call-footer">
            <span className="call-time">Створено: {createdAt}</span>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CallItem;