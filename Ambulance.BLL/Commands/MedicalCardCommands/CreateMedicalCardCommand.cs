using Ambulance.Core;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;

namespace Ambulance.BLL.Commands.CallsCommands
{
    public class CreateMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly int patientId;

        public CreateMedicalCardCommand(int patientId, IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext)
            : base(operateUnitOfWork, mapper, userContext)
        {
            this.patientId = patientId;
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

            LogAction($"{Name} для пацієнта {patientId}");
            return true;
        }
    }
}

