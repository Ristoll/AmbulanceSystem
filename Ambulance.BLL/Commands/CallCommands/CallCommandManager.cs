using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.BLL.Commands.PersonIdentity.PICommands;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallCommands
{
    public class CallCommandManager : AbstractCommandManager
    {
        public CallCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }

        public bool CreateCallCommand(CallDto callDto, int actionOwnerID)
        {
            var command = new CreateCallCommand(callDto, unitOfWork, mapper, actionOwnerID);
            return ExecuteCommand(command, "Не вдалося створити користувача");
        }
        public bool DeleteCallCommand(int callId, int actionOwnerID)
        {
            var command = new DeleteCallCommand(callId, unitOfWork, mapper, actionOwnerID);
            return ExecuteCommand(command, "Не вдалося видалити виклик");
        }
        public bool FillCallCommand(CallDto callDto, PatientDto patientDto, PersonCreateRequest personCreateRequest, int actionOwnerID)
        {
            var command = new FillCallCommand(callDto, patientDto, actionOwnerID, personCreateRequest, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося заповнити виклик");
        }
        public CallDto LoadCallCommand(int callId)
        {
            var command = new LoadCallCommand(unitOfWork, mapper, callId);
            return ExecuteCommand(command, "Не вдалося завантажити виклик")!;
        }
        public List<CallDto> LoadCallsCommand()
        {
            var command = new LoadCallsCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити виклики");
        }
        public PersonProfileDTO SearchPersonCommand(PersonProfileDTO personProfileDTO)
        {
            var command = new SearchPersonCommand(personProfileDTO, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти користувача")!;
        }
    }
}
