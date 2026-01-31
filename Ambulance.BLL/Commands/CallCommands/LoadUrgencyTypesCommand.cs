using AutoMapper;
using Ambulance.DTO;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities.StandartEnums;

namespace Ambulance.BLL.Commands.CallCommands;

internal class LoadUrgencyTypesCommand : AbstrCommandWithDA<List<UrgencyTypeDto>>
{
    public override string Name => "Отримати типи терміновості";

    public LoadUrgencyTypesCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper) { }

    public override List<UrgencyTypeDto> Execute()
    {
        return Enum.GetValues<UrgencyType>()
             .Select(u => new UrgencyTypeDto
             {
               Level = (int)u,
               Name = u.ToString()
             })
             .ToList();
    }
}