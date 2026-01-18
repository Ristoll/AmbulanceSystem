using Ambulance.Core;
using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
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
                .FirstOrDefault(mr => mr.MedicalCardId == medicalCardId);
            if (medicalCard == null)
                throw new InvalidOperationException($"Медичну картку з ID {medicalCardId} не знайдено");
            return mapper.Map<MedicalCardDto>(medicalCard);
        }

    }
}
