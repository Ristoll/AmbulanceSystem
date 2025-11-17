using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    public class MedicalCardCommandManager : AbstractCommandManager
    {
        public MedicalCardCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
       : base(unitOfWork, mapper) { }

        public bool CreateMedicalCardCommand(MedicalCardDto medicalCardDto)
        {
            var command = new CreateMedicalCardCommand(medicalCardDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося створити медичну карту");
        }
        public bool UpdateMedicalCardCommand(MedicalCardDto medicalCardDto)
        {
            var command = new UpdateMedicalCardCommand(medicalCardDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося оновити медичну карту");
        }
        public bool SearchMeducalCardCommand(int personId)
        {
            var command = new SearchMedicalCardCommand(personId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти медичну карту");
        }
        public bool CreateMedicalRecordCommand(MedicalRecordDto medicalRecordDto)
        {
            var command = new CreateMedicalRecordCommand(medicalRecordDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося створити медичний запис");
        }
    }
}
