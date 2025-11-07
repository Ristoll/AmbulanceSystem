namespace Ambulance.BLL.Models;

public class FullAnalyticsDTO
{
    public CallAnalyticsDTO Calls { get; set; } = new();
    public List<BrigadeResourceDTO> Brigades { get; set; } = new();
    public List<DecAllergAnalyticsDTO> Deceases { get; set; } = new();
}
