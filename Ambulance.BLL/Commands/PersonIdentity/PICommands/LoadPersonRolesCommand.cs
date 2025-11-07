using Ambulance.Core.Entities;
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
        var roles = dAPoint.UserRoleRepository.GetAll();

        if (roles == null || !roles.Any())
            throw new InvalidOperationException("Не знайдено жодної ролі користувачів");

        // витягуємо лише поле Name у список рядків
        var roleNames = roles
            .Where(r => !string.IsNullOrWhiteSpace(r.Name))
            .Select(r => r.Name)
            .ToList();

        return roleNames!;
    }
}
