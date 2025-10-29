using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

public class DeceaseAnalyticsCommand : AbstrCommandWithDA<List<DecAllergAnalyticsModel>>
{
    private readonly DateTime from;
    private readonly DateTime to;

    public override string Name => "Аналітика хронічних захворювань";

    public DeceaseAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext, DateTime from, DateTime to)
        : base(operateUnitOfWork, mapper, userContext)
    {
        if (from > to)
            throw new ArgumentException("'From' не може бути більше за 'To'.");

        this.from = from;
        this.to = to;
    }

    public override List<DecAllergAnalyticsModel> Execute()
    {
        // уточнити щодо часу записів захворювань та алергій
        return new List<DecAllergAnalyticsModel>();
    }
}
