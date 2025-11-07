using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;

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
        // уточнити щодо станів речей бригад та конкретної аналітики по бриагадах
        return new List<BrigadeResourceDTO>();
    }
}
