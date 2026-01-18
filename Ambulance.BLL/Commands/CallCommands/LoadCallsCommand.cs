using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Commands.CallCommands
{
    public class LoadCallsCommand : AbstrCommandWithDA<List<CallDto>>
    {
        public override string Name => "Отримати всі виклики";

        public LoadCallsCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public override List<CallDto> Execute()
        {
            var calls = dAPoint.CallRepository
                .GetQueryable()
                .Include(x => x.Patient)
                .Include(x => x.Dispatcher)
                .Include(x => x.Hospital)
                .Include(x => x.Brigades)
                   .ThenInclude(x => x.BrigadeType)
                .OrderByDescending(x => x.StartCallTime)
                .ToList();

            if (!calls.Any())
                return new List<CallDto>();

            return mapper.Map<List<CallDto>>(calls);
        }
    }
}
