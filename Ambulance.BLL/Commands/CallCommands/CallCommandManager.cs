using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.BLL.Commands.PersonIdentity.PICommands;
using Ambulance.DTO;
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

        //public bool DeleteCallCommand(int callId)
        //{
        //    var command = new DeleteCallCommand(callId, unitOfWork, mapper);
        //    return ExecuteCommand(command, "Не вдалося видалити виклик");
        //}
        //public int CreateAndFillCallCommand(CallDto callDto, PatientCreateRequest? request)
        //{
        //    var command = new CreateAndFillCallCommand(callDto, request, unitOfWork, mapper);
        //    return ExecuteCommand(command, "Не вдалося створити та заповнити виклик");
        //}
        public CallDto? LoadCallCommand(int callId)
        {
            var command = new LoadCallCommand(unitOfWork, mapper, callId);
            return ExecuteCommand(command, "Не вдалося завантажити виклик")!;
        }
        public List<CallDto> LoadCallsCommand()
        {
            var command = new LoadCallsCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити виклики");
        }
        //public List<HospitalDto> LoadHospitalsCommand()
        //{
        //    var command = new LoadHospitalsCommand(unitOfWork, mapper);
        //    return ExecuteCommand(command, "Не вдалося завантажити виклики");
        //}
        public IEnumerable<PersonProfileDTO> SearchPatientCommand(string? text)
        {
            var command = new SearchPatientCommand(text, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти користувача")!;
        }
    }
}
