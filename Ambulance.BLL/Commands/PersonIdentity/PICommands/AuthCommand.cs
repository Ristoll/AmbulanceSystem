using AutoMapper;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class AuthCommand : AbstrCommandWithDA<Person>
{
    override public string Name => "Автентифікація Person";
    private readonly string login;
    private readonly string password;

    public AuthCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, string login, string password)
        : base(operateUnitOfWork, mapper)
    {
        this.login = login;
        this.password = password;
    }

    public override Person Execute()
    {
        var existingPerson = dAPoint.PersonRepository.FirstOrDefault(p => p.Login == login);
        ArgumentNullException.ThrowIfNull(existingPerson, "Невірний логін або пароль");

        var passwordValid = PasswordHasher.VerifyPassword(password, existingPerson.PasswordHash);
        ArgumentNullException.ThrowIfNull(passwordValid, "Невірний логін або пароль");

        return existingPerson;
    }
}
