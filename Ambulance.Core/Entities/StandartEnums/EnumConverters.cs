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

    public static string ParseBrigadeState(string? state)
       => !string.IsNullOrEmpty(state) && Enum.TryParse<BrigadeState>(state, out var s)
           ? s.ToString()
           : BrigadeState.Unknown.ToString();

    public static string GetBrigadeStateUkrainian(string stateCode)
    {
        if (Enum.TryParse<BrigadeState>(stateCode, out var state))
        {
            return state switch
            {
                BrigadeState.Free => "Вільна",
                BrigadeState.OnTheCall => "У виїзді",
                BrigadeState.OnBreak => "На перерві",
                BrigadeState.Offline => "Неактивна",
                _ => "Невідомий статус"
            };
        }
        return "Невідомий статус";
    }
}
