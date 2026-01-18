using Ambulance.DTO.PersonModels;
using AmbulanceSystem.DTO;

namespace Ambulance.DTO
{
    public class FillCallFullRequest
    {
        public CallDto Call { get; set; } = null!;
        public PatientCreateRequest? Person { get; set; }
    }
}
