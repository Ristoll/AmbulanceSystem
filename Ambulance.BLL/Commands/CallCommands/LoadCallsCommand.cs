using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.BLL.Commands.CallCommands;

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
            .Include(x => x.Person)
            .Include(x => x.Dispatcher)
            .Include(x => x.Hospital)
            .Include(x => x.MedicalRecord)
            .OrderByDescending(x => x.CallId) // перші будуть останні виклики
            .ToList();

        if (!calls.Any())
            return new List<CallDto>();

        return mapper.Map<List<CallDto>>(calls);
    }
}
