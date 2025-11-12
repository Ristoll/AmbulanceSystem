using Ambulance.BLL.Models;
using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

public class AnaliticsCommandManager : AbstractCommandManager
{
    public AnaliticsCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
    : base(unitOfWork, mapper) { }

    public List<BrigadeResourceDTO> GetBrigadeResourceAnalytics(DateTime from, DateTime to, int actionOwnerId)
    {
        var command = new BrigadeResourceAnalyticsCommand(unitOfWork, mapper, from, to, actionOwnerId);
        return command.Execute();
    }
    public CallAnalyticsDTO GetCallAnalytics(DateTime from, DateTime to, int actionOwnerId)
    {
        var command = new CallAnalyticsCommand(unitOfWork, mapper, from, to, actionOwnerId);
        return command.Execute();
    }
    public List<DecAllergAnalyticsDTO>  GetDeceaseAnalytics(int actionOwnerId)
    {
        var command = new DeceaseAnalyticsCommand(unitOfWork, mapper, actionOwnerId);
        return command.Execute();
    }
    public FullAnalyticsDTO GetFullAnalytics(DateTime from, DateTime to, int actionOwnerId)
    {
        var command = new FullAnalyticsCommand(unitOfWork, mapper, from, to, actionOwnerId);
        return command.Execute();
    }
}
