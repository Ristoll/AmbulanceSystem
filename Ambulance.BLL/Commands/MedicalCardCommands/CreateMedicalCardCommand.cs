using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using AmbulanceSystem.DTO;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    public class CreateMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly MedicalCardDto medicalCardDto;
        private readonly int actorId;

        public CreateMedicalCardCommand(MedicalCardDto medicalCardDto, int actorId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.medicalCardDto = medicalCardDto;
            this.actorId = actorId;
        }

        public override string Name => "Створення медичної картки";

        public override bool Execute()
        {
            var medicalCard = mapper.Map<MedicalCard>(medicalCardDto);

            dAPoint.MedicalCardRepository.Add(medicalCard);
            dAPoint.Save();

            LogAction($"{Name} для пацієнта {medicalCardDto.PatientId}", actorId);
            return true;
        }
    }
}

