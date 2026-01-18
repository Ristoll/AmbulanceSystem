using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using AmbulanceSystem.DTO;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    public class UpdateMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly MedicalCardDto medicalCardDto;

        public UpdateMedicalCardCommand(MedicalCardDto medicalCardDto, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.medicalCardDto = medicalCardDto;
        }

        public override string Name => "Оновлення медичної картки";

        public override bool Execute()
        {
            var existingCard = dAPoint.MedicalCardRepository
                .FirstOrDefault(mc => mc.PersonId == medicalCardDto.PatientId);

            if (existingCard == null)
                throw new InvalidOperationException($"Медичну картку для пацієнта {medicalCardDto.PatientId} не знайдено");

            mapper.Map(medicalCardDto, existingCard);

            dAPoint.MedicalCardRepository.Update(existingCard);
            dAPoint.Save();

            return true;
        }
    }
}
