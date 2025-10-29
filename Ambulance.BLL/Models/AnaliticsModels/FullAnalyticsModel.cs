namespace Ambulance.BLL.Models;

public class FullAnalyticsModel
{
    public CallAnalyticsModel Calls { get; set; } = new();
    public List<BrigadeResourceModel> Brigades { get; set; } = new();
    public List<DecAllergAnalyticsModel> Deceases { get; set; } = new();
}
