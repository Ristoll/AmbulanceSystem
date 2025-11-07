using AmbulanceSystem.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class AdminResetPasswordCommand : AbstrCommandWithDA<bool>
{
    private readonly int targetPersonId;
    private readonly string newPassword;
    private readonly int adminId;

    public override string Name => "Скидання паролю адміністратором";

    public AdminResetPasswordCommand(IUnitOfWork uow, IMapper mapper, string newPassword, int targetPersonId, int adminId)
        : base(uow, mapper)
    {
        ValidateIn(newPassword, adminId);

        this.newPassword = newPassword;
        this.targetPersonId = targetPersonId;
        this.adminId = adminId;
    }

    public override bool Execute()
    {
        var person = dAPoint.PersonRepository.GetById(targetPersonId);

        if (person == null)
            throw new InvalidOperationException("Користувача не знайдено");

        person.PasswordHash = PasswordHasher.HashPassword(newPassword);
        dAPoint.Save();

        LogAction($"{Name}: Адмін {adminId} змінив пароль користувача {person.Login}", adminId);
        return true;
    }

    private void ValidateIn(string newPassword, int adminId)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("Новий пароль не може бути порожнім");

        if (newPassword.Length < 8)
            throw new ArgumentException("Новий пароль повинен містити не менше 8 символів");

        var admin = dAPoint.PersonRepository.GetQueryable()
            .Include(x => x.UserRole)
            .FirstOrDefault(p => p.PersonId == adminId);

        if (admin == null)
            throw new ArgumentException($"Адміністратора з ID {adminId} не знайдено");

        if (!string.Equals(admin.UserRole?.Name, "admin", StringComparison.OrdinalIgnoreCase)) // строга перевірка на роль "admin"
            throw new UnauthorizedAccessException("Тільки адміністратор може виконати скидання паролю");
    }
}
