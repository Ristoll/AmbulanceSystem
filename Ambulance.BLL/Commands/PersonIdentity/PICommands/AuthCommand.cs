using Ambulance.Core.Entities.StandartEnums;
using Ambulance.DTO.PersonModels;
using Ambulance.ExternalServices;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class AuthCommand : AbstrCommandWithDA<AuthResponse>
{
    override public string Name => "Автентифікація Person";
    private readonly LoginRequest request;

    public AuthCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, LoginRequest request)
        : base(operateUnitOfWork, mapper)
    {
        ArgumentNullException.ThrowIfNull(request.Login, "Логін не може бути null");
        ArgumentNullException.ThrowIfNull(request.Password, "Пароль не може бути null");

        this.request = request;
    }

    public override AuthResponse Execute()
    {
        var existingPerson = dAPoint.PersonRepository
            .FirstOrDefault(p => p.Login == request.Login);

        if (existingPerson == null)
        {
            existingPerson = dAPoint.PersonRepository
                .FirstOrDefault(p => p.PhoneNumber == request.Login); // додаткова перевірка по номеру телефону, бо логін може бути номером для нових
        }

        if (existingPerson == null || !PasswordHasher.VerifyPassword(request.Password, existingPerson.PasswordHash!))
        {
            throw new UnauthorizedAccessException("Невірний логін або пароль");
        }

        var result = mapper.Map<AuthResponse>(existingPerson);

        var payload = new Dictionary<string, object>
        {
            ["sub"] = existingPerson.PersonId,
            ["login"] = existingPerson.Login ?? existingPerson.PhoneNumber,
            ["role"] = existingPerson.UserRole ?? UserRole.Unknown.ToString()
        };

        result.JwtToken = JWTService.GenerateJwtToken(payload); // генерація токена окремо

        return result;
    }
}
