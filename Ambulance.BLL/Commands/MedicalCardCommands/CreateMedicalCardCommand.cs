using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.CallsCommands
{
    public class CreateMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly int patientId;
        private readonly int actorId;

        public CreateMedicalCardCommand(int patientId, int actorId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.patientId = patientId;
            this.actorId = actorId;
        }

        public override string Name => "Створення медичної картки";

        public override bool Execute()
        {
            // Перевірка, чи медкарта вже існує
            var existingCard = dAPoint.MedicalCardRepository.GetById(patientId);
            if (existingCard != null)
                return false; // Карта вже існує

            // Створення сутності медкарти
            var medicalCard = new MedicalCard
            {
                PersonId = patientId,
                CreationDate = DateTime.Now
            };

            dAPoint.MedicalCardRepository.Add(medicalCard);
            dAPoint.Save();

            LogAction($"{Name} для пацієнта {patientId}", actorId);
            return true;
        }
    }
}

