using Ambulance.Core.Entities.StandartEnums;

namespace AmbulanceSystem.Core.Entities;

public static class EnumConverters
{
    public static string ToEnumString<T>(this string? value, T defaultValue)
        where T : struct, Enum // struct тут потрібно, щоб додатково зазначити компілятору, 
        // що це value тип, який не може бути nullable
    {
        if (value == null)
            return defaultValue.ToString();

        return Enum.TryParse<T>(value, true, out var result)
            ? result.ToString()
            : defaultValue.ToString();
    }

    public static string ToEnumString<T>(this int? levelValue, T defaultValue)
        where T : struct, Enum // struct тут потрібно, щоб додатково зазначити компілятору, 
        // що це value тип, який не може бути nullable
    {
        if (levelValue == null)
            return defaultValue.ToString();

        if (Enum.IsDefined(typeof(T), levelValue))
        {
            return ((T)(object)levelValue).ToString();
        }

        return defaultValue.ToString();
    }

    // Додай цей метод у свій EnumConverters
    public static string ToEnumString<T>(this int levelValue, T defaultValue)
        where T : struct, Enum
    {
        // Просто передаємо значення в метод для int?, він все зробить сам
        return ToEnumString((int?)levelValue, defaultValue);
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
