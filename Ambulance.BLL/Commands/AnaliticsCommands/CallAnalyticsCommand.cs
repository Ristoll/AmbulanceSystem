using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

public class CallAnalyticsCommand : AbstrCommandWithDA<CallAnalyticsModel>
{
    private readonly DateTime from;
    private readonly DateTime to;

    public override string Name => "Аналітика дзвінків";

    public CallAnalyticsCommand (IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext, DateTime from, DateTime to)
        : base(operateUnitOfWork, mapper, userContext)
    {
        if (from > to)
            throw new ArgumentException("From не може бути більше за to");

        this.from = from;
        this.to = to;
    }

    public override CallAnalyticsModel Execute()
    {
        var calls = dAPoint.CallRepository.GetAll()
            .Where(c => c.StartCallTime >= from && c.StartCallTime <= to)
            .ToList();

        var completed = calls.Count(c => c.CompletionTime != null);

        var avgResponse = calls
            .Where(c => c.ArrivalTime != null && c.StartCallTime != null)
            .Select(c => (c.ArrivalTime!.Value - c.StartCallTime!.Value).TotalMinutes) // до хвилин
            .DefaultIfEmpty(0) // якщо немає викликів це 0, щоб уникнути помилки
            .Average();

        var byUrgency = calls
            .GroupBy(c => c.UrgencyType)
            .ToDictionary(g => g.Key, g => g.Count()); // тимчасове рішення --- перепитати щодо сутності

        return new CallAnalyticsModel
        {
            TotalCalls = calls.Count,
            CompletedCalls = completed,
            AverageResponseMinutes = avgResponse,
            CallsByUrgency = byUrgency
        };
    }
}
