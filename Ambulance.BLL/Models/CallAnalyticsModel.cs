namespace Ambulance.BLL.Models;

public class CallAnalyticsModel
{
    public int TotalCalls { get; set; }
    public int CompletedCalls { get; set; }
    public double AverageResponseMinutes { get; set; }
    public Dictionary<string, int> CallsByUrgency { get; set; } = new(); // тип терміновості та кількість викликів
}
