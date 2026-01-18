namespace AmbulanceSystem.DTO;

public class BrigadeDto
{
    public int BrigadeId { get; set; }
    public int HospitalId { get; set; }
    public int BrigadeStateId { get; set; }
    public int BrigadeTypeId { get; set; }
    public int? CurrentCallId { get; set; }
    public string BrigadeStateName { get; set; } = string.Empty; // Українська назва для відображення
    public string BrigadeStateCode { get; set; } = string.Empty; // Англійський код для фронтенду
    public string BrigadeTypeName { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public TimeSpan EstimatedArrival { get; set; }
    public double EstimatedDistanceKm { get; set; }
    public List<BrigadeMemberDto>? Members { get; set; }
}
