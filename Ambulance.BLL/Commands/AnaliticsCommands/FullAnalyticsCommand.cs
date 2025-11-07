using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

public class FullAnalyticsCommand : AbstrCommandWithDA<FullAnalyticsDTO>
{
    private readonly DateTime from;
    private readonly DateTime to;

    public override string Name => "Повна аналітика для адміністратора";

    public FullAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, DateTime from, DateTime to)
        : base(operateUnitOfWork, mapper)
    {
        if (from > to)
            throw new ArgumentException("From не може бути більше за to");

        this.from = from;
        this.to = to;
    }

    public override FullAnalyticsDTO Execute()
    {
        var callAnalytics = new CallAnalyticsCommand(dAPoint, mapper, from, to).Execute();
        var brigadeAnalytics = new BrigadeResourceAnalyticsCommand(dAPoint, mapper, from, to).Execute();
        var deceaseAnalytics = new DeceaseAnalyticsCommand(dAPoint, mapper, from, to).Execute();

        return new FullAnalyticsDTO
        {
            Calls = callAnalytics,
            Brigades = brigadeAnalytics,
            Deceases = deceaseAnalytics
        };
    }
}

