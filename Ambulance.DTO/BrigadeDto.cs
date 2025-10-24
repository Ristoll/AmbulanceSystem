namespace AmbulanceSystem.DTO;

public class BrigadeDto
{
    public int BrigadeId { get; set; }
    public int HospitalId { get; set; }
    public int BrigadeStateId { get; set; }
    public int BrigadeTypeId { get; set; }
    public int? CurrentCallId { get; set; }
    public string BrigadeStateName { get; set; } = string.Empty;
    public string BrigadeTypeName { get; set; } = string.Empty;
    public List<BrigadeMemberDto>? Members { get; set; }
}

