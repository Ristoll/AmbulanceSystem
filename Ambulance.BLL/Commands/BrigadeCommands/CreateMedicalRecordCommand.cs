using Ambulance.BLL.Commands;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.BrigadeCommands
{
    internal class CreateMedicalRecordCommand : AbstrCommandWithDA<bool>
    {
        private readonly MedicalRecordModel medicalRecordModel;


        public CreateMedicalRecordCommand(MedicalRecordModel medicalCardModel, IUnitOfWork operateUnitOfWork, IMapper mapper) : base(operateUnitOfWork, mapper)
        {
            this.medicalRecordModel = medicalCardModel;
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
