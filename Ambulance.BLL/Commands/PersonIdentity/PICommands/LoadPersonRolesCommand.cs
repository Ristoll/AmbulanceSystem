using Ambulance.Core.Entities.StandartEnums;
using Ambulance.DTO;
using Ambulance.DTO.EnumOptimDTO;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class LoadPersonRolesCommand : AbstrCommandWithDA<List<UserRoleDto>>
{
    public override string Name => "Отримати ролі користувачів";

    public LoadPersonRolesCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper) { }

    public override List<UserRoleDto> Execute()
    {
        return Enum.GetValues<UserRole>()
             .Select(u => new UserRoleDto
             {
                 Level = (int)u,
                 RoleName = u.ToString()
             })
             .ToList();
    }
}
