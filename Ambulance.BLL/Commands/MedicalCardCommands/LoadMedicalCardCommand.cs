using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.MedicalCardCommands;

public class LoadMedicalCardCommand : AbstrCommandWithDA<MedicalCardDto>
{
        private readonly int medicalCardId;
        public LoadMedicalCardCommand(int medicalCardId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.medicalCardId = medicalCardId;
        }
        public override string Name => "Завантаження медичного запису";
        public override MedicalCardDto Execute()
        {
            var medicalCard = dAPoint.MedicalRecordRepository
                .FirstOrDefault(mr => mr.CardId == medicalCardId);
            if (medicalCard == null)
                throw new InvalidOperationException($"Медичну картку з ID {medicalCardId} не знайдено");
            return mapper.Map<MedicalCardDto>(medicalCard);
        }
}
