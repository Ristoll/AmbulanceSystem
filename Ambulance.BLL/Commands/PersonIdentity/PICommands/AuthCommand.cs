using Ambulance.BLL.Models;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AutoMapper;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AmbulanceSystem.Core.Entities;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class AuthCommand : AbstrCommandWithDA<AuthResponseModel>
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

    public override AuthResponseModel Execute()
    {
        var existingPerson = dAPoint.PersonRepository.FirstOrDefault(p => p.Login == login);
        ArgumentNullException.ThrowIfNull(existingPerson, "Невірний логін або пароль");

        if (!PasswordHasher.VerifyPassword(password, existingPerson.PasswordHash))
            throw new UnauthorizedAccessException("Невірний логін або пароль");

        var result = mapper.Map<AuthResponseModel>(existingPerson);
        result.JwtToken = GenerateJwtToken(existingPerson); // генерація токена окремо

        return result;
    }

    // тимчасове рішення, пізніше винести в окремий сервіс
    public string GenerateJwtToken(Person user)
    {
        var header = new { alg = "HS256", typ = "JWT" };

        var payload = new
        {
            sub = user.PersonId, // тема токену
            name = user.Login,
            role = user.Role.ToString()
        };

        string headerJson = JsonSerializer.Serialize(header);
        string payloadJson = JsonSerializer.Serialize(payload);

        string headerEncoded = StaticSC.Base64UrlEncode(headerJson);
        string payloadEncoded = StaticSC.Base64UrlEncode(payloadJson);

        string unsignedToken = $"{headerEncoded}.{payloadEncoded}";

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(StaticSC.secretcode));
        var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(unsignedToken));

        string signatureEncoded = StaticSC.Base64UrlEncode(signature); // без JSON

        string jwt = $"{headerEncoded}.{payloadEncoded}.{signatureEncoded}";

        return jwt;
    }
}
