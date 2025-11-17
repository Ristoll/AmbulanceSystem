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
    internal class CreateMedicalRecordCommand : AbstrCommandWithDA<bool>
    {
        private readonly MedicalRecordDto medicalRecordDto;

        public CreateMedicalRecordCommand(MedicalRecordDto medicalRecordDto, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            : base(operateUnitOfWork, mapper)
        {
            this.medicalRecordDto = medicalRecordDto;
        }

        public override string Name => "Створення медичного запису";

        public override bool Execute()
        {
            var medicalRecordEntity = mapper.Map<MedicalRecord>(medicalRecordDto);
            dAPoint.MedicalRecordRepository.Add(medicalRecordEntity);
            dAPoint.Save();
            return true;
        }
    }
}
