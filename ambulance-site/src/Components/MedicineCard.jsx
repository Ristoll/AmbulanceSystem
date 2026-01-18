import './MedicineCard.css';

function MedicineCard({ id, name, itemType, unitType, quantity, expiryDate, imageUrl, onSelect }) {

  const getMedicineImage = () => {
    return imageUrl && imageUrl.trim() !== ""
      ? imageUrl
      : "/images/medicine-default.png"; // стандартна картинка якщо немає фото
  };

  const formatExpiryDate = (date) => {
    if (!date) return "-";
    return date.toString();
  };

  return (
    <div className="medicine-card" onClick={() => onSelect?.(id)}>
      <div className="medicine-image">
        <img 
          src={getMedicineImage()} 
          alt={name} 
          className="medicine-img"
        />
      </div>

      <div className="medicine-info">
        <p className="medicine-title">
          <strong>{name}</strong>
        </p>

        <p className="medicine-unit">
          Тип: {itemType || "—"}
        </p>

        <p className="medicine-quantity">
          Кількість: {quantity} {unitType || "одиниць"}
        </p>

        <p className="medicine-expiry">
          Придатне до: {formatExpiryDate(expiryDate)}
        </p>
      </div>
    </div>
  );
}

export default MedicineCard;
