using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class AuthCommand : AbstrCommandWithDA<PersonExtModel>
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

    public override PersonExtModel Execute()
    {
        var existingPerson = dAPoint.PersonRepository.FirstOrDefault(p => p.Login == login);
        ArgumentNullException.ThrowIfNull(existingPerson, "Невірний логін або пароль");

        if (!PasswordHasher.VerifyPassword(password, existingPerson.PasswordHash))
            throw new ArgumentException("Невірний логін або пароль");

        return mapper.Map<PersonExtModel>(existingPerson); // повертаємо модель потрібну нам, jwt токен буде потім
    }
}
