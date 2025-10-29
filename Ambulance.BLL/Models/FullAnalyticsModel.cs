namespace Ambulance.BLL.Models;

public class FullAnalyticsModel
{
    public CallAnalyticsModel Calls { get; set; } = new();
    public List<BrigadeResourceModel> Brigades { get; set; } = new();
    public List<DecAAnalyticsModel> Deceases { get; set; } = new();
}
