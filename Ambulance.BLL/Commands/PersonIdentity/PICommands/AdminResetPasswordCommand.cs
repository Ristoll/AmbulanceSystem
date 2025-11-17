using Ambulance.ExternalServices;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class AdminResetPasswordCommand : AbstrCommandWithDA<bool>
{
    private readonly int targetPersonId;
    private readonly string newPassword;
    private readonly int adminId;

    public override string Name => "Скидання паролю адміністратором";

    public AdminResetPasswordCommand(IUnitOfWork uow, IMapper mapper, string newPassword, int targetPersonId)
        : base(uow, mapper)
    {
        ValidateIn(newPassword);

        this.newPassword = newPassword;
        this.targetPersonId = targetPersonId;
    }

    public override bool Execute()
    {
        var person = dAPoint.PersonRepository.GetById(targetPersonId);

        if (person == null)
            throw new InvalidOperationException("Користувача не знайдено");

        person.PasswordHash = PasswordHasher.HashPassword(newPassword);
        dAPoint.Save();

        return true;
    }

    private void ValidateIn(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("Новий пароль не може бути порожнім");

        if (newPassword.Length < 8)
            throw new ArgumentException("Новий пароль повинен містити не менше 8 символів");
    }
}
