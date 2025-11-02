using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class LoadPersonRolesCommand : AbstrCommandWithDA<List<UserRoleViewModel>>
{
    public override string Name => "Отримати ролі користувачів";

    public LoadPersonRolesCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper) { }

    public override List<UserRoleViewModel> Execute()
    {
        var roles = dAPoint.UserRoleRepository.GetAll();
        return mapper.Map<List<UserRoleViewModel>>(roles);
    }
}