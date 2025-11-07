using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Ambulance.ExternalServices;
/// <summary>
/// Сервіс для генерації та перевірки JWT токенів.
/// Тимчасово зберігає секретний ключ у коді.
/// У продакшн-версії ключ має зберігатися у AWS Secrets Manager або в змінних середовища.
/// </summary>
public static class JWTService
{
    public static string secretcode = "cAtwa1kkEy"; //секретний код для jwt токенів

    // застосовується для генерації JWT токенів
    public static string Base64UrlEncode(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    public static string Base64UrlEncode(byte[] bytes) // перевантаження одразу для байтового масиву
    {
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    // тимчасово без параметру int expiresInMinutes = 60 - термін дії токена в хвилинах
    public static string GenerateJwtToken(Dictionary<string, object> payload)
    {
        var header = new { alg = "HS256", typ = "JWT" };

        // додаємо дату закінчення дії токену
        //payload["exp"] = DateTimeOffset.UtcNow.AddMinutes(expiresInMinutes).ToUnixTimeSeconds();

        string headerJson = JsonSerializer.Serialize(header);
        string payloadJson = JsonSerializer.Serialize(payload.OrderBy(k => k.Key)); // сортуємо для консистентності та запобіганню майбутнім змінам

        string headerEncoded = Base64UrlEncode(headerJson);
        string payloadEncoded = Base64UrlEncode(payloadJson);

        string unsignedToken = $"{headerEncoded}.{payloadEncoded}";

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretcode));
        var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(unsignedToken));

        string signatureEncoded = Base64UrlEncode(signature); // без JSON

        string jwt = $"{headerEncoded}.{payloadEncoded}.{signatureEncoded}";

        return jwt;
    }
}
