using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Commands.CallCommands
{
    public class LoadCallsCommand : AbstrCommandWithDA<List<Call>>
    {
        public override string Name => "Отримати всі виклики";

        public LoadCallsCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public override List<Call> Execute()
        {
            return dAPoint.CallRepository.GetAll().ToList();           
        }
    }
}
