import { useState, useEffect } from 'react';
import Modal from './Modal';
import './NotificationModal.css';

function NotificationModal({ notifications = [], onClose }) {
  /*щоб не намагалося виділити відсутнє повідомлення якщо їх немає*/
  const [activeId, setActiveId] = useState(notifications.length ? notifications[0].id : null);

  useEffect(() => {
    if (notifications.length && !notifications.find(n => n.id === activeId)) {
      setActiveId(notifications[0].id);
    }
  }, [notifications]);

  const active = notifications.find(n => n.id === activeId);

  return (
    <Modal isOpen={true} onClose={onClose} contentClassName="modal-large">
      <div className="notif-modal">
        <div className="notif-list">
          {notifications.map(n => (
            <div
              key={n.id}
              className={`notif-list-item ${n.id === activeId ? 'active' : ''}`}
              onClick={() => setActiveId(n.id)}
              role="button"
              tabIndex={0}
              onKeyDown={(e) => { if (e.key === 'Enter') setActiveId(n.id); }}
            >
              <div className="notif-side">Системне</div>
              <div className="notif-summary">
                <strong className="notif-title">{n.title}</strong>
                <div className="notif-time">{new Date(n.createdAt).toLocaleString()}</div>
                <p className="notif-preview">{n.message.length > 120 ? n.message.slice(0, 120) + '…' : n.message}</p>
              </div>
            </div>
          ))}

        </div>

        <div className="notif-detail">
          {active ? (
            <div className="notif-detail-inner">
              <h3 className="detail-title">{active.title}</h3>
              <div className="detail-time">{new Date(active.createdAt).toLocaleString()}</div>
              <div className="detail-body">{active.message}</div>
            </div>
          ) : (
            <div className="notif-empty">Виберіть повідомлення зліва</div>
          )}
        </div>
      </div>
    </Modal>
  );
}

export default NotificationModal;
