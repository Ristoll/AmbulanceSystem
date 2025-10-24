using Ambulance.BLL.Models;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class ChangePasswordCommand : AbstrCommandWithDA<bool>
{
    override public string Name => "Зміна паролю Person";
    private readonly ChangePasswordModel changePasswordModel;

    public ChangePasswordCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, ChangePasswordModel changePasswordModel)
        : base(operateUnitOfWork, mapper)
    {
        BaseDataChecker(changePasswordModel);

        this.changePasswordModel = changePasswordModel;
    }

    public override bool Execute()
    {
        var existingPerson = dAPoint.PersonRepository
                .FirstOrDefault(p => p.PersonId == changePasswordModel.PersonID);

        ArgumentNullException.ThrowIfNull(existingPerson, "Користувач не знайдений");

        if (!PasswordHasher.VerifyPassword(changePasswordModel.OldPassword, existingPerson.PasswordHash))
        {
            throw new ArgumentException("Невірний старий пароль");
        }

        existingPerson.PasswordHash = PasswordHasher.HashPassword(changePasswordModel.NewPassword);

        dAPoint.Save(); // EF автоматично згенерує UPDATE тільки для PasswordHash
        LogAction($"Був змінений пароль: {existingPerson.Login}", existingPerson.PersonId);
        return true;
    }

    private void BaseDataChecker(ChangePasswordModel changePasswordModel)
    {
        ArgumentNullException.ThrowIfNull(changePasswordModel, "Модель зміни паролю відсутня");

        if (string.IsNullOrWhiteSpace(changePasswordModel.OldPassword))
            throw new ArgumentException("Старий пароль не може бути порожнім");

        if (string.IsNullOrWhiteSpace(changePasswordModel.NewPassword) || changePasswordModel.NewPassword.Length < 8)
            throw new ArgumentException("Новий пароль повинен містити не менше 8 символів");
    }
}

