using Ambulance.BLL.Models;
using Ambulance.ExternalServices;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class AuthCommand : AbstrCommandWithDA<AuthResponseModel>
{
    override public string Name => "Автентифікація Person";
    private readonly string login;
    private readonly string password;

    public AuthCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, string login, string password)
        : base(operateUnitOfWork, mapper)
    {
        ArgumentNullException.ThrowIfNull(login, "Логін не може бути null");
        ArgumentNullException.ThrowIfNull(password, "Пароль не може бути null");    

        this.login = login;
        this.password = password;
    }

    public override AuthResponseModel Execute()
    {
        var existingPerson = dAPoint.PersonRepository.FirstOrDefault(p => p.Login == login);
        ArgumentNullException.ThrowIfNull(existingPerson, "Невірний логін або пароль");

        if (!PasswordHasher.VerifyPassword(password, existingPerson.PasswordHash!)) // припускаємо, що PasswordHash не null до корекції БД
            throw new UnauthorizedAccessException("Невірний логін або пароль");

        var result = mapper.Map<AuthResponseModel>(existingPerson);

        var payload = new Dictionary<string, object>
        {
            ["sub"] = existingPerson.PersonId,
            ["login"] = existingPerson.Login!, // тимчасово припускаємо, що Login не null до корекції БД
            ["role"] = existingPerson.UserRole?.Name ?? "Unknown"
        };

        result.JwtToken = JWTService.GenerateJwtToken(payload); // генерація токена окремо

        LogAction($"{Name}: Person {existingPerson.Login} автентифікувався", existingPerson.PersonId);
        return result;
    }
}
