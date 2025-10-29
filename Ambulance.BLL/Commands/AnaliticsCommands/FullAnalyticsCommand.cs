using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

public class FullAnalyticsCommand : AbstrCommandWithDA<FullAnalyticsModel>
{
    private readonly DateTime from;
    private readonly DateTime to;

    public override string Name => "Повна аналітика для адміністратора";

    public FullAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext, DateTime from, DateTime to)
        : base(operateUnitOfWork, mapper, userContext)
    {
        if (from > to)
            throw new ArgumentException("From не може бути більше за to");

        this.from = from;
        this.to = to;
    }

    public override FullAnalyticsModel Execute()
    {
        var callAnalytics = new CallAnalyticsCommand(dAPoint, mapper, userContext, from, to).Execute();
        var brigadeAnalytics = new BrigadeResourceAnalyticsCommand(dAPoint, mapper, userContext, from, to).Execute();
        var deceaseAnalytics = new DeceaseAnalyticsCommand(dAPoint, mapper, userContext, from, to).Execute();

        return new FullAnalyticsModel
        {
            Calls = callAnalytics,
            Brigades = brigadeAnalytics,
            Deceases = deceaseAnalytics
        };
    }
}

