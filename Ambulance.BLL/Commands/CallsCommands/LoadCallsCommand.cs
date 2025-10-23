using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Commands.CallsCommands
{
    public class LoadCallsCommand : AbstrCommandWithDA<List<CallModel>>
    {
        public override string Name => "Отримати всі виклики";

        public LoadCallsCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public override List<CallModel> Execute()
        {
            var calls = dAPoint.CallRepository.GetAll().ToList();
            var callsModels = calls.Select(c => mapper.Map<CallModel>(c)).ToList();

            LogAction($"{Name}: отримано {callsModels.Count} викликів");

            return callsModels;
        }
    }
}
