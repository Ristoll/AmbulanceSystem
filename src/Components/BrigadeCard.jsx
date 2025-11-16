import './BrigadeCard.css';

function BrigadeCard({ id, status, type, members, onSelect }) {

  const getBrigadeImage = () => {
    switch(status){
      case 'Вільна': return "/images/ambulance-available.png";
      case 'У виїзді': return "/images/ambulance-busy.png";
      default: return "/images/ambulance-title.png"
    }
  };

  return (
    <div className="brigade-card" onClick={() => onSelect?.(id)}>
      <div className="brigade-image">
        <img 
          src={getBrigadeImage()} 
          alt={`Бригада ${type}`} 
          className={`brigade-img ${status === 'Вільна' ? 'img-available' : 'img-busy'}`}
        />
      </div>

      <div className="brigade-info">
        <p className="brigade-title"><strong>Бригада №{id}</strong></p>
        <p className={`brigade-status ${status === 'Вільна' ? 'status-free' : 'status-busy'}`}>
            {status}
        </p>
        <p className="brigade-type">Тип: {type}</p>
        <p className="brigade-members">Учасники: {members.join(', ')}</p>
      </div>
    </div>
  );
}

export default BrigadeCard;