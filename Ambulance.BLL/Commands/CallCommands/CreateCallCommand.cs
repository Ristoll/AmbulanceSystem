using Ambulance.Core;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallCommands;
public class CreateCallCommand : AbstrCommandWithDA<bool>
{
    private readonly CallDto callDto;
    private readonly int personId;

    public override string Name => "Створення виклику";
    public CreateCallCommand(CallDto callModel, IUnitOfWork unitOfWork, IMapper mapper, int personId)
        : base(unitOfWork, mapper)
    {
        this.callDto = callModel;
        this.personId = personId;
    }

    public override bool Execute()
    {
        var call= mapper.Map<Call>(callDto);

        dAPoint.CallRepository.Add(call);
        dAPoint.Save();
        LogAction($"{Name} № {call.CallId}", personId);
        return true;
    }
}