namespace Ambulance.Core.Entities.StandartEnums;

public enum UrgencyType // запитати Крістіну, можливо для БД буде краще зробити так само,
                        // щоб зберігати число, а не рядок
{
    NonUrgent = 1,
    Urgent = 2,
    Emergency = 3,
    Unknown = 4
}