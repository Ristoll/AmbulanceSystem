using Ambulance.Core;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
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
        private readonly PersonExtModel patientModel;

        public CreateMedicalCardCommand(PersonExtModel patientModel, IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext) 
            :base(operateUnitOfWork, mapper, userContext)
        {
            this.patientModel = patientModel;
        }

        public override string Name => "Створення медичної картки";

        public override bool Execute()
        {
            var patient = dAPoint.PersonRepository.GetById(patientModel.PersonId);
            MedicalCardModel medicalCardModel = new MedicalCardModel
            {
                PatientId = patientModel.PersonId,
                CreationDate = DateTime.Now
            };
            return true;
        }
    }
}
