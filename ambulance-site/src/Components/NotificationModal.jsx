import { useState, useEffect } from "react";
import Modal from "./Modal";
import "./NotificationModal.css";

function NotificationModal({ notifications = [], onClose }) {
  // Use index-based selection so we work with messages that may not have stable `id` fields
  const [activeIndex, setActiveIndex] = useState(
    notifications.length ? 0 : null
  );

  useEffect(() => {
    if (!notifications || notifications.length === 0) {
      setActiveIndex(null);
      return;
    }

    if (activeIndex === null || activeIndex >= notifications.length) {
      setActiveIndex(0);
    }
  }, [notifications]);

  const active = activeIndex !== null ? notifications[activeIndex] : null;

  // отримання типу повідомлення українською
  const getNotificationType = (notification) => {
    switch (notification.type) {
      case "Personal":
        return "Особисте";
      case "Service":
        return "Системне";
      case "Urgent":
        return "Термінове";
      default:
        return "Системне";
    }
  };

  // отримання CSS класу для типу повідомлення (тут їх забагато, тому виокремив функцію)
  const getNotificationTypeClass = (notification) => {
    switch (notification.type) {
      case "Personal":
        return "notif-personal";
      case "Service":
        return "notif-service";
      case "Urgent":
        return "notif-urgent";
      default:
        return "notif-service";
    }
  };

  return (
    <Modal isOpen={true} onClose={onClose} contentClassName="modal-large">
      <div className="notif-modal">
        <div className="notif-list">
          {notifications.map((n, idx) => ( // на свякий додав, бо інколи id перестає фіксуватися в списку
            <div
              key={n.id ?? idx}
              className={`notif-list-item ${idx === activeIndex ? "active" : ""} ${getNotificationTypeClass(n)}`}
              onClick={() => setActiveIndex(idx)}
              role="button"
              tabIndex={0}
            >
              <div className={`notif-side ${getNotificationTypeClass(n)}`}>
                {getNotificationType(n)}
              </div>
              <div className="notif-summary">
                <strong className="notif-title">{n.title}</strong>
                <div className="notif-time">
                  {(() => {
                    const t = n.time;
                    return t ? new Date(t).toLocaleString() : "";
                  })()}
                </div>
                <p className="notif-preview">
                  {n.message.length > 120
                    ? n.message.slice(0, 120) + "…"
                    : n.message}
                </p>
              </div>
            </div>
          ))}
        </div>

        <div className="notif-detail">
              {active ? (
            <div className="notif-detail-inner">
              <div
                className={`detail-header ${getNotificationTypeClass(active)}`}
              >
                <span className="detail-type">
                  {getNotificationType(active)}
                </span>
                <h3 className="detail-title">{active.title}</h3>
                    <div className="detail-time">
                      {(() => {
                        const t = active.time;
                        return t ? new Date(t).toLocaleString() : "";
                      })()}
                    </div>
              </div>
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