using Ambulance.DTO.PersonModels;
using Ambulance.ExternalServices;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class ChangePasswordCommand : AbstrCommandWithDA<bool>
{
    override public string Name => "Зміна паролю Person";
    private readonly ChangePasswordRequest changePasswordModel;
    private readonly int userId;

    public ChangePasswordCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, ChangePasswordRequest changePasswordModel, int userId)
        : base(operateUnitOfWork, mapper)
    {
        ValidateIn(changePasswordModel);

        this.changePasswordModel = changePasswordModel;
        this.userId = userId;
    }

    public override bool Execute()
    {
        var person = dAPoint.PersonRepository.GetById(userId);

        if (person == null)
            throw new InvalidOperationException("Користувача не знайдено");

        if (string.IsNullOrEmpty(person.PasswordHash) ||
            !PasswordHasher.VerifyPassword(changePasswordModel.OldPassword, person.PasswordHash))
        {
            throw new ArgumentException("Невірний старий пароль");
        }

        person.PasswordHash = PasswordHasher.HashPassword(changePasswordModel.NewPassword);

        dAPoint.Save(); // EF автоматично згенерує UPDATE тільки для PasswordHash

        return true;
    }

    private void ValidateIn(ChangePasswordRequest changePasswordModel)
    {
        ArgumentNullException.ThrowIfNull(changePasswordModel, "Модель зміни паролю відсутня");

        if (string.IsNullOrWhiteSpace(changePasswordModel.OldPassword))
            throw new ArgumentException("Старий пароль не може бути порожнім");

        if (string.IsNullOrWhiteSpace(changePasswordModel.NewPassword) || changePasswordModel.NewPassword.Length < 8)
            throw new ArgumentException("Новий пароль повинен містити не менше 8 символів");

        if (changePasswordModel.OldPassword == changePasswordModel.NewPassword)
            throw new ArgumentException("Паролі не повинны збігатися");
    }
}
