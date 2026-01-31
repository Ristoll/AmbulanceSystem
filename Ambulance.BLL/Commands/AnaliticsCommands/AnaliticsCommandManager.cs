using AmbulanceSystem.Core;
using AutoMapper;

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
    public Dictionary<int, int> GetCallAnalytics(AnalyticsPeriod period, DateTime startDate)
    {
        var command = new CallAnalyticsCommand(unitOfWork, mapper, period, startDate);
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
