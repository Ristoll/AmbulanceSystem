using AutoMapper;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class CreatePCommand : AbstrCommandWithDA <bool>
{
    override public string Name => "Створення Person";
    private readonly string login;
    private readonly string password;
    private readonly int actionOwberID;

    public CreatePCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, int actionOwberID, string login, string password)
        : base(operateUnitOfWork, mapper)
    {
        BaseDataChecker(actionOwberID, login, password);

        this.login = login;
        this.password = password;
        this.actionOwberID = actionOwberID;
    }

    public override bool Execute()
    {
        var alreadyExistingPerson = dAPoint.PersonRepository.FirstOrDefault(p => p.Login == login);

        if (alreadyExistingPerson != null)
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

        LogAction($"Був створений новий Person: {login}", actionOwberID);
        return true;
    }

    private void BaseDataChecker(int actionOwberID, string login, string password)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(login, "Логін не може бути пустим");
        ArgumentNullException.ThrowIfNullOrEmpty(password, "Пароль не може бути пустим");

        if(actionOwberID < 0)
        {
            throw new ArgumentException("Ідентифікатор виконавця дії некоректний");
        }

        if(login.Length < 5 || login.Length > 20)
        {
            throw new ArgumentException("Логін повинен містити від 5 до 20 символів");
        }

        if(password.Length < 8)
        {
            throw new ArgumentException("Пароль повинен містити не менше 8 символів");
        }
    }
}
