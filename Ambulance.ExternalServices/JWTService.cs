using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ambulance.ExternalServices;

/// <summary>
/// Сервіс для генерації та перевірки JWT токенів.
/// Тимчасово зберігає секретний ключ у коді.
/// У продакшн-версії ключ має зберігатися у AWS Secrets Manager або в інших середовищах із захистом.
/// </summary>
public static class JWTService
{
    public static string secretcode = "cAtwa1kkEy123456!@#123456ahahhahah256bitcamooon"; //секретний код для jwt токенів

    // застосовується для генерації JWT токенів
    public static string GenerateJwtToken(Dictionary<string, object> payload)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretcode));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        foreach (var item in payload)
        {
            if (item.Value != null)
            {
                claims.Add(new Claim(item.Key, item.Value.ToString()));
            }
        }

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(12), // Термін дії
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
