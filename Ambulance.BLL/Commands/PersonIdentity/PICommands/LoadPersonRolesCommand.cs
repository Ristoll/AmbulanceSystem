using AutoMapper;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities.StandartEnums;

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
