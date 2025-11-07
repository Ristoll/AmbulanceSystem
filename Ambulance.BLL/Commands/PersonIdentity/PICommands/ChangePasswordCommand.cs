using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class ChangePasswordCommand : AbstrCommandWithDA<bool>
{
    override public string Name => "Зміна паролю Person";
    private readonly ChangePasswordRequest changePasswordModel;
    private readonly int personId;

    public ChangePasswordCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, ChangePasswordRequest changePasswordModel, int personId)
        : base(operateUnitOfWork, mapper)
    {
        ValidateIn(changePasswordModel, personId);

        this.changePasswordModel = changePasswordModel;
        this.personId = personId;
    }

    public override bool Execute()
    {
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
        LogAction($"{Name}: Був змінений пароль: {person.Login}", personId);

        return true;
    }

    private void ValidateIn(ChangePasswordRequest changePasswordModel, int personId)
    {
        base.ValidateIn(personId); // перевірка існування personId

        ArgumentNullException.ThrowIfNull(changePasswordModel, "Модель зміни паролю відсутня");

        if (string.IsNullOrWhiteSpace(changePasswordModel.OldPassword))
            throw new ArgumentException("Старий пароль не може бути порожнім");

        if (string.IsNullOrWhiteSpace(changePasswordModel.NewPassword) || changePasswordModel.NewPassword.Length < 8)
            throw new ArgumentException("Новий пароль повинен містити не менше 8 символів");

        if (changePasswordModel.OldPassword == changePasswordModel.NewPassword)
            throw new ArgumentException("Паролі не повинны збігатися");
    }
}
