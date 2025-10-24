using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallsCommands
{
    public class CreateMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly PatientModel patientModel;

        public CreateMedicalCardCommand(PatientModel patientModel, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            :base(operateUnitOfWork, mapper)
        {
            this.patientModel = patientModel;
        }

        public override string Name => "Створення медичної картки";

        public override bool Execute()
        {
            var patient = dAPoint.PatientRepository.GetById(patientModel.PatientId);
            MedicalCardModel medicalCardModel = new MedicalCardModel
            {
                PatientId = patientModel.PatientId,
                CreationDate = DateTime.Now
            };
            return true;
        }
    }
}
