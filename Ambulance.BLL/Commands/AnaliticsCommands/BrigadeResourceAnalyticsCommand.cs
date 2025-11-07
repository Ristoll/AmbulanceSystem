using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;
using System.Linq;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

public class BrigadeResourceAnalyticsCommand : AbstrCommandWithDA<List<BrigadeResourceDTO>>
{
    private readonly DateTime from;
    private readonly DateTime to;

    public override string Name => "Аналітика ресурсів бригад";

    public BrigadeResourceAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, DateTime from, DateTime to)
        : base(operateUnitOfWork, mapper)
    {
        if (from > to)
            throw new ArgumentException("From не може бути більше за to");

        this.from = from;
        this.to = to;
    }

    public override List<BrigadeResourceDTO> Execute()
    {
        var brigades = dAPoint.BrigadeRepository.GetAll().ToList();
        var calls = dAPoint.CallRepository.GetAll().ToList();
        var brigadeItems = dAPoint.BrigadeItemRepository.GetAll().ToList();

        var result = new List<BrigadeResourceDTO>();

        foreach (var brigade in brigades)
        {
            // Виклики цієї бригади
            var brigadeCalls = calls
                .Where(c => c.Brigades != null && c.Brigades.Contains(brigade))
                .ToList();

            // Кількість оброблених викликів
            var totalCalls = brigadeCalls.Count;

            // Середній час виклику в хвилинах
            var avgCallDuration = brigadeCalls
                .Where(c => c.CompletionTime.HasValue && c.StartCallTime.HasValue)
                .Select(c => (c.CompletionTime.Value - c.StartCallTime.Value).TotalMinutes)
                .DefaultIfEmpty(0)
                .Average();

            // Кількість унікальних ресурсів
            var distinctItems = brigadeItems
                .Where(bi => bi.BrigadeId == brigade.BrigadeId && !string.IsNullOrWhiteSpace(bi.Item.Name))
                .Select(bi => bi.Item.Name)
                .Distinct()
                .Count();

            result.Add(new BrigadeResourceDTO
            {
                BrigadeId = brigade.BrigadeId,
                BrigadeType = brigade.BrigadeTypeId.ToString(),
                TotalCallsHandled = totalCalls,
                DistinctItemsUsed = distinctItems,
                AverageCallDurationMinutes = avgCallDuration
            });
        }

        return result;
    }


}
