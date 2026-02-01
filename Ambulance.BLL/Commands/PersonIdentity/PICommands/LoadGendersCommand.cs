using Ambulance.DTO.EnumOptimDTO;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class LoadGendersCommand : AbstrCommandWithDA<List<GenderDto>>
{
    public override string Name => "Отримати гендери";

    public LoadGendersCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper) { }

    public override List<GenderDto> Execute()
    {
        return Enum.GetValues<Gender>()
             .Select(u => new GenderDto
             {
                 Level = (int)u,
                 Name = u.ToString()
             })
             .ToList();
    }
}
