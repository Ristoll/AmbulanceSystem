import "./BrigadeCard.css";

function BrigadeCard({ id, status, type, members, onSelect }) {
  // status - це BrigadeStateCode
  const getBrigadeImage = () => {
    switch (status) {
      case "Free":
        return "/images/ambulance-available.png";
      case "OnTheCall":
        return "/images/ambulance-busy.png";
      case "OnBreak":
        return "/images/ambulance-break.png";
      case "Offline":
        return "/images/ambulance-offline.png";
      default:
        return "/images/ambulance-title.png";
    }
  };

  // для отримання української назви статусу
  const getUkrainianStatus = (statusCode) => {
    switch (statusCode) {
      case "Free":
        return "Вільна";
      case "OnTheCall":
        return "У виїзді";
      case "OnBreak":
        return "На перерві";
      case "Offline":
        return "Неактивна";
      default:
        return "Невідомий статус";
    }
  };

  const ukrainianStatus = getUkrainianStatus(status);

  return (
    <div className="brigade-card" onClick={() => onSelect?.(id)}>
      <div className="brigade-image">
        <img
          src={getBrigadeImage()}
          alt={`Бригада ${type}`}
          className={`brigade-img ${
            status === "Free"
              ? "img-available"
              : status === "OnTheCall"
              ? "img-busy"
              : status === "OnBreak"
              ? "img-break"
              : "img-offline"
          }`}
        />
      </div>

      <div className="brigade-info">
        <p className="brigade-title">
          <strong>Бригада №{id}</strong>
        </p>
        <p
          className={`brigade-status ${
            status === "Free"
              ? "status-free"
              : status === "OnTheCall"
              ? "status-busy"
              : status === "OnBreak"
              ? "status-break"
              : "status-offline"
          }`}
        >
          {ukrainianStatus}
        </p>
        <p className="brigade-type">Тип: {type}</p>
        <p className="brigade-members">Учасники: {members.join(", ")}</p>
      </div>
    </div>
  );
}

export default BrigadeCard;
