using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Commands.CallCommands
{
    public class LoadCallCommand : AbstrCommandWithDA<CallDto>
    {
        public override string Name => "Отримати виклик за ідентифікатором";
        private readonly int callId;
        public LoadCallCommand(IUnitOfWork unitOfWork, IMapper mapper,  int callId)
            : base(unitOfWork, mapper)
        {
            this.callId = callId;
        }
        public override CallDto Execute()
        {
            var call = dAPoint.CallRepository.GetById(callId);
            if (call == null)
                return null;
            return mapper.Map<CallDto>(call);
        }
    }
}
