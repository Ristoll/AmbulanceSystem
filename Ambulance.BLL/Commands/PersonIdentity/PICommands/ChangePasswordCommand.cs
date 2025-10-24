using AutoMapper;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class ChangePasswordCommand : AbstrCommandWithDA<bool>
{
    override public string Name => "Створення Person";
    private readonly string oldPassword;
    private readonly string newPassword;
    private readonly Person actionOwber;

    public ChangePasswordCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, string PresonID, string oldPassword, string newPasswod)
        : base(operateUnitOfWork, mapper)
    {
        BaseDataChecker(PresonID, password);

        ArgumentNullException.ThrowIfNull(actionOwber, "Виконавець дії відсутній");

        this.oldPassword = oldPassword;
        this.newPassword = newPassword;
        this.actionOwber = actionOwber;
    }

    public override bool Execute()
    {
        var existingPerson = dAPoint.PersonRepository.GetAll()
            .FirstOrDefault(p => p.Login == login);

        if (existingPerson != null)
        {
            return false;
        }

        var newPerson = new Person
        {
            Login = login,
            PasswordHash = PasswordHasher.HashPassword(password)
        };

        dAPoint.PersonRepository.Add(newPerson);
        dAPoint.Save();
        LogAction($"Був створений новий Person: {login}", actionOwber);
        return true;
    }

    private void BaseDataChecker(string login, string password)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(login, "Логін не може бути пустим");
        ArgumentNullException.ThrowIfNullOrEmpty(password, "Пароль не може бути пустим");

        if (login.Length < 5 || login.Length > 20)
        {
            throw new ArgumentException("Логін повинен містити від 5 до 20 символів");
        }

        if (password.Length < 8)
        {
            throw new ArgumentException("Пароль повинен містити не менше 8 символів");
        }
    }
}

