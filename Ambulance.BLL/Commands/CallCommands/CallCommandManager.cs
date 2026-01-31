using AutoMapper;
using Ambulance.DTO;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using Ambulance.DTO.PersonModels;

namespace Ambulance.BLL.Commands.CallCommands
{
    public class CallCommandManager : AbstractCommandManager
    {
        public CallCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }

        public bool DeleteCallCommand(int callId)
        {
            var command = new DeleteCallCommand(callId, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося видалити виклик");
        }
        public int CreateAndFillCallCommand(CallDto callDto, PatientCreateRequest? request)
        {
            var command = new CreateAndFillCallCommand(callDto, request, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося створити та заповнити виклик");
        }
        public CallDto? LoadCallCommand(int callId)
        {
            var command = new LoadCallCommand(unitOfWork, mapper, callId);
            return ExecuteCommand(command, "Не вдалося завантажити виклик")!;
        }

        public List<UrgencyTypeDto> LoadUrgencyTypesCommand()
        {
            var command = new LoadUrgencyTypesCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося отримати список рівнів терміновості");
        }
        
        public List<CallDto> LoadCallsCommand()
        {
            var command = new LoadCallsCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити виклики");
        }
        public IEnumerable<PersonProfileDTO> SearchPatientCommand(string? text)
        {
            var command = new SearchPatientCommand(text, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти користувача")!;
        }
    }
}
