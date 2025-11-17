using Ambulance.Core.Entities.StandartEnums;

namespace AmbulanceSystem.Core.Entities;

public static class EnumConverters
{
    public static string ParseUserRole(string? role)
       => !string.IsNullOrEmpty(role) && Enum.TryParse<UserRole>(role, out var r)
           ? r.ToString()
           : UserRole.Unknown.ToString();

    public static string ParseUserGender(string? gender)
        => !string.IsNullOrEmpty(gender) && Enum.TryParse<Gender>(gender, out var g)
            ? g.ToString()
            : Gender.Other.ToString();
}
