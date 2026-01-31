using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.BLL.Commands.CallCommands;

public class LoadCallCommand : AbstrCommandWithDA<CallDto?>
{
    public override string Name => "Отримати виклик за ідентифікатором";
    private readonly int callId;

    public LoadCallCommand(IUnitOfWork unitOfWork, IMapper mapper,  int callId)
        : base(unitOfWork, mapper)
    {
        this.callId = callId;
    }

    public override CallDto? Execute()
    {
        var call = dAPoint.CallRepository
            .GetQueryable()
            .Include(x => x.Person)
            .Include(x => x.Dispatcher)
            .Include(x => x.Hospital)
            .Include(x => x.MedicalRecord)
            .FirstOrDefault(x => x.CallId == callId);

        if (call == null)
            return null;

        return mapper.Map<CallDto>(call);
    }
}
