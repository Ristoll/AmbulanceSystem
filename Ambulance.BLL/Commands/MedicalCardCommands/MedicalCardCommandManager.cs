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

        public bool CreateMedicalCardCommand(MedicalCardDto medicalCardDto, int actionOwnerID)
        {
            var command = new CreateMedicalCardCommand(medicalCardDto, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося створити медичну карту");
        }
        public bool UpdateMedicalCardCommand(MedicalCardDto medicalCardDto, int actionOwnerID)
        {
            var command = new UpdateMedicalCardCommand(medicalCardDto, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося оновити медичну карту");
        }
        public bool SearchMeducalCardCommand(int personId, int actionOwnerID)
        {
            var command = new SearchMedicalCardCommand(personId, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти медичну карту");
        }
        public bool CreateMedicalRecordCommand(MedicalRecordDto medicalRecordDto, int actionOwnerID)
        {
            var command = new CreateMedicalRecordCommand(medicalRecordDto, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося створити медичний запис");
        }
    }
}
