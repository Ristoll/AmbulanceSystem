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
    public class LoadMedicalRecordCommand : AbstrCommandWithDA<MedicalRecordDto>
    {
        private readonly int medicalRecordId;
        public LoadMedicalRecordCommand(int medicalRecordId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.medicalRecordId = medicalRecordId;
        }
        public override string Name => "Завантаження медичного запису";
        public override MedicalRecordDto Execute()
        {
            var medicalRecord = dAPoint.MedicalRecordRepository
                .FirstOrDefault(mr => mr.MedicalRecordId == medicalRecordId);
            if (medicalRecord == null)
                throw new InvalidOperationException($"Медичний запис з ID {medicalRecordId} не знайдено");
            return mapper.Map<MedicalRecordDto>(medicalRecord);
        }

    }
}
