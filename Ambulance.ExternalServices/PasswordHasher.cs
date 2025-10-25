using System.Security.Cryptography;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        // стврорення хешу на основі пароля та солі
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');

        if (parts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var expectedHash = Convert.FromBase64String(parts[1]);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        var actualHash = pbkdf2.GetBytes(32);

        // перевіряємо всі байти хешу на відповідність (щоб зловмисник не міг відстежити який саме байт неправильний)
        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }
}
