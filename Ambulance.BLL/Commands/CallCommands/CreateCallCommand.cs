using Ambulance.Core;
using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
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


    public override string Name => "Створення виклику";
    public CreateCallCommand(CallDto callDto, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.callDto = callDto;
    }

    public override bool Execute()
    {
        var call= mapper.Map<Call>(callDto);

        dAPoint.CallRepository.Add(call);
        dAPoint.Save();

        return true;
    }
}