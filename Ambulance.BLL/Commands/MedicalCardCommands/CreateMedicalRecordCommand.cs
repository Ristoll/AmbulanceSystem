using Ambulance.Core;
using AmbulanceSystem.BLL.Models;
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
        private readonly MedicalRecordDto medicalRecordModel;


        public CreateMedicalRecordCommand(MedicalRecordDto medicalCardDto, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            : base(operateUnitOfWork, mapper)
        {
            medicalRecordModel = medicalCardDto;
        }

        public override string Name => "Створення медичного запису";

        public override bool Execute()
        {
            var medicalRecordEntity = mapper.Map<MedicalRecord>(medicalRecordModel);
            dAPoint.MedicalRecordRepository.Add(medicalRecordEntity);
            dAPoint.Save();
            return true;
        }
    }
}
