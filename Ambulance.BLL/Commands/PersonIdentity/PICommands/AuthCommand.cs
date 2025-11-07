using Ambulance.DTO.PersonModels;
using Ambulance.ExternalServices;
using AmbulanceSystem.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        var existingPerson = dAPoint.PersonRepository.GetQueryable()
            .Include(p => p.UserRole)
            .FirstOrDefault(p => p.Login == request.Login);

        if (existingPerson == null || !PasswordHasher.VerifyPassword(request.Password, existingPerson.PasswordHash!))
        {
            throw new UnauthorizedAccessException("Невірний логін або пароль");
        }

        var result = mapper.Map<AuthResponse>(existingPerson);

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
