using Ambulance.Core.Entities;
using Ambulance.Core.Entities.StandartEnums;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class LoadPersonRolesCommand : AbstrCommandWithDA<List<string>>
{
    public override string Name => "Отримати ролі користувачів";

    public LoadPersonRolesCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper) { }

    public override List<string> Execute()
    {
        return Enum.GetNames(typeof(UserRole)).ToList();
    }
}
