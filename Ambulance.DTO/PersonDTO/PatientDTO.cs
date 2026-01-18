using AmbulanceSystem.DTO;

namespace Ambulance.DTO.PersonModels;

public class PatientDto
{
    public int PersonId { get; set; }
    public List<CallDto> Calls { get; set; } = new();
    public MedicalCardDto? MedicalCard { get; set; }
}
