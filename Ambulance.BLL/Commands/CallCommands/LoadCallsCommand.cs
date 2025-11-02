using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Commands.CallCommands
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

            return callsModels;
        }
    }
}
