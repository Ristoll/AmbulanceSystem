using Ambulance.BLL.Models;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

public class DeceaseAnalyticsCommand : AbstrCommandWithDA<List<DecAllergAnalyticsDTO>>
{
    private readonly DateTime from;
    private readonly DateTime to;

    public override string Name => "Аналітика хронічних захворювань";

    public DeceaseAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, DateTime from, DateTime to)
        : base(operateUnitOfWork, mapper)
    {
        if (from > to)
            throw new ArgumentException("'From' не може бути більше за 'To'.");

        this.from = from;
        this.to = to;
    }

    public override List<DecAllergAnalyticsDTO> Execute()
    {
        // уточнити щодо часу записів захворювань та алергій
        return new List<DecAllergAnalyticsDTO>();
    }
}
