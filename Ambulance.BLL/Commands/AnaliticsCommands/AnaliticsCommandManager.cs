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

    public Dictionary<string, int> GetResourcesAnalytics()
    {
        var command = new ResourcesAnalyticsCommand(unitOfWork, mapper);
        return command.Execute();
    }
    public Dictionary<DateTime, int> GetCallAnalytics()
    {
        var command = new CallAnalyticsCommand(unitOfWork, mapper);
        return command.Execute();
    }
    public Dictionary<string, int>  GetDeceaseAnalytics()
    {
        var command = new DeceaseAnalyticsCommand(unitOfWork, mapper);
        return command.Execute();
    }
    public Dictionary<string, int> GetAllergyAnalytics()
    {
        var command = new AllergyAnalyticsCommand(unitOfWork, mapper);
        return command.Execute();
    }
}
