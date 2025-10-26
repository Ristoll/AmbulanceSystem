namespace AmbulanceSystem.Core.Entities;

public static class EnumConverters
{
    public static Gender ParseUserGender(string? v)
        => !string.IsNullOrEmpty(v) && Enum.TryParse<Gender>(v, out var result) ? result : Gender.Other;
}
