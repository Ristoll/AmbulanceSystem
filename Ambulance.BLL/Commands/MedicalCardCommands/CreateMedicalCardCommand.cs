using Ambulance.Core;
using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    public class CreateMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly MedicalCardDto medicalCardDto;

        public CreateMedicalCardCommand(MedicalCardDto medicalCardDto, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.medicalCardDto = medicalCardDto;
        }

        public override string Name => "Створення медичної картки";

        public override bool Execute()
        {
            var medicalCard = mapper.Map<MedicalCard>(medicalCardDto);

            dAPoint.MedicalCardRepository.Add(medicalCard);
            dAPoint.Save();

            return true;
        }
    }
}

