using Ambulance.DTO.PersonModels;
using AmbulanceSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.DTO
{
    public class FillCallRequest
    {
        public CallDto CallDto { get; set; } = null!;
        public PatientDto PatientDto { get; set; } = null!;
        public PersonCreateRequest PersonCreateRequest { get; set; } = null!;
    }
}
