using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.DTO;

public class PatientDto
{
    public int PatientId { get; set; }
    public int PersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string SecondName { get; set; } = string.Empty;
    public List<string> Allergies { get; set; } = new List<string>();
    public List<string> ChronicDiseases { get; set; } = new List<string>();
    public List<CallDto> Calls { get; set; } = new List<CallDto>();
    public List<MedicalCardDto> MedicalCards { get; set; } = new List<MedicalCardDto>();
}

