import './Modal.css';

function Modal({ isOpen, onClose, children, contentClassName }) { /* пропси для керування модалкою */
  if (!isOpen) return null; // не рендеримо, якщо закрито

  return (
    <div className="modal-overlay" onClick={onClose}> {/* закрити при кліку поза вікном */}
      <div className={`modal-content ${contentClassName || ''}`} onClick={e => e.stopPropagation()}> {/* зупиняємо поширення кліку всередині вікна */}
        <button className="modal-close" onClick={onClose}>×</button>
        {children}
      </div>
    </div>
  );
}

export default Modal;
