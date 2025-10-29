using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class ChangePasswordCommand : AbstrCommandWithDA<bool>
{
    override public string Name => "Зміна паролю Person";
    private readonly ChangePasswordModel changePasswordModel;

    public ChangePasswordCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext, ChangePasswordModel changePasswordModel)
        : base(operateUnitOfWork, mapper, userContext)
    {
        BaseDataChecker(changePasswordModel);

        this.changePasswordModel = changePasswordModel;
    }
    public override bool Execute()
    {
        if (userContext.CurrentUserId == null)
            return false;

        int personId = userContext.CurrentUserId.Value;
        var person = dAPoint.PersonRepository.GetById(personId);

        if (person == null)
            throw new InvalidOperationException("Користувача не знайдено");

        if (string.IsNullOrEmpty(person.PasswordHash) ||
            !PasswordHasher.VerifyPassword(changePasswordModel.OldPassword, person.PasswordHash))
        {
            throw new ArgumentException("Невірний старий пароль");
        }

        person.PasswordHash = PasswordHasher.HashPassword(changePasswordModel.NewPassword);

        dAPoint.Save(); // EF автоматично згенерує UPDATE тільки для PasswordHash
        LogAction($"{Name}: Був змінений пароль: {person.Login}");

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

