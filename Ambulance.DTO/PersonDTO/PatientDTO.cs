using AmbulanceSystem.DTO;

namespace Ambulance.DTO.PersonModels;

public class PatientDto : PersonProfileDTO
{
    public int PersonId { get; set; }
    public List<string> Allergies { get; set; } = new();
    public List<string> ChronicDiseases { get; set; } = new();
    public List<CallDto> Calls { get; set; } = new();
    public List<MedicalCardDto> MedicalCards { get; set; } = new();
}
