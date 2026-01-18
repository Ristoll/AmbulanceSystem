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
        public bool UpdateMedicalRecordCommand(MedicalRecordDto medicalRecordDto)
        {
            var command = new UpdateMedicalRecordCommand(medicalRecordDto, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося оновити медичний запис");
        }
        public List<MedicalRecordDto> LoadMedicalRecordsCommand(int medicalCardId)
        {
            var command = new LoadMedicalRecordsCommand(medicalCardId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити медичні записи");
        }
        public MedicalRecordDto LoadMedicalRecordCommand(int medicalRecordId)
        {
            var command = new LoadMedicalRecordCommand(medicalRecordId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити медичний запис за ідентифікатором");
        }
        public List<MedicalCardDto> LoadAllMedicalCardsCommand()
        {
            var command = new LoadAllMedicalCardsCommand( unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити медичні карти");
        }
        public MedicalCardDto LoadMedicalCardCommand(int medicalCardId)
        {
            var command = new LoadMedicalCardCommand(medicalCardId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити медичну карту за ідентифікатором");
        }

    }
}
