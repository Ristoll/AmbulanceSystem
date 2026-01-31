using Ambulance.Core.Entities.StandartEnums;

namespace AmbulanceSystem.Core.Entities;

public static class EnumConverters
{
    // замінив на рощширення стрінгу для зручності
    public static string ToEnumString<T>(this string? value, T defaultValue)
        where T : struct, Enum // struct тут потрібно, щоб додатково зазначити компілятору, 
        // що це value тип, який не може бути nullable
    {
        return Enum.TryParse<T>(value, true, out var result)
            ? result.ToString()
            : defaultValue.ToString();
    }

    // обговорить с Кристиной
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
