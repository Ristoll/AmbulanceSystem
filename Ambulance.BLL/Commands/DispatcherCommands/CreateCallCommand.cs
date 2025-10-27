using Ambulance.BLL.Commands;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallsCommands;
public class CreateCallCommand : AbstrCommandWithDA<bool>
{
    private readonly CallModel callModel;

    public override string Name => "Створення виклику";
    public CreateCallCommand(CallModel callModel, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.callModel = callModel;
    }

    public override bool Execute()
    {
        var newCallModel = new CallModel
        {
            StartCallTime = DateTime.Now,
            DispatcherId = callModel.DispatcherId
        };

        var newCall= mapper.Map<Call>(newCallModel);

        dAPoint.CallRepository.Add(newCall);
        dAPoint.Save();
        LogAction($"{Name} № {newCall.CallId}", 3);
        return true;
    }
}