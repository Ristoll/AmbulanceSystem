namespace Ambulance.BLL;

public static class StaticSC
{
    public static string secretcode = "cAtwa1kkEy"; // секретний код для jwt токенів

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
}
