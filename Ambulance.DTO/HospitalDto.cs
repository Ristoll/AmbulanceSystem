using AmbulanceSystem.DTO;

namespace Ambulance.DTO;

public class HospitalDto
{
    public int HospitalId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<BrigadeDto> Brigades { get; set; } = new List<BrigadeDto>();
}
