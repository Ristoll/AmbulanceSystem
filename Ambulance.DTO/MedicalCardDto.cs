using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.DTO;
public class MedicalCardDto
{
    public int MedicalCardId { get; set; }
    public int PatientId { get; set; }
    public DateTime? CreationDate { get; set; }
    public string? BloodType { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public string? Notes { get; set; }
    public List<string> Allergies { get; set; } = new List<string>();
    public List<string> ChronicDiseases { get; set; } = new List<string>();
    public List<MedicalRecordDto> MedicalRecords { get; set; } = new List<MedicalRecordDto>();
}



