namespace Ambulance.BLL.Models;

public class CallAnalyticsDTO
{
    public int TotalCalls { get; set; }
    public int CompletedCalls { get; set; }
    public double AverageResponseMinutes { get; set; }
    public Dictionary<int, int> CallsByUrgency { get; set; } = new(); // тип терміновості та кількість викликів
}
