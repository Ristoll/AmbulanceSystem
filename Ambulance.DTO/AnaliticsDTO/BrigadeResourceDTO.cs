namespace Ambulance.BLL.Models;

public class BrigadeResourceDTO
{
    public int BrigadeId { get; set; }
    public string? BrigadeType { get; set; }
    public int TotalCallsHandled { get; set; }
    public int DistinctItemsUsed { get; set; }
}
