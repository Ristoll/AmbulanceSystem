namespace Ambulance.BLL.Models;

public class DecAllergAnalyticsModel
{
    // назва захворювання та кількість пацієнтів із цим захворюванням
    public Dictionary<string, int> DeceaseStatistics { get; set; } = new();

    // назва алергії та кількість пацієнтів із цією алергією
    public Dictionary<string, int> AllergyStatistics { get; set; } = new();
}
