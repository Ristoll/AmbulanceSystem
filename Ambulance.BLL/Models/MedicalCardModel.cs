using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.BLL.Models;
public class MedicalCardModel
{
    public int MedicalCardId { get; set; }
    public int PatientId { get; set; }
    public DateTime? CreationDate { get; set; }
    public int Age { get; set; }
    public string? BloodType { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public string? Notes { get; set; }
    public List<string> Allergies { get; set; } = new List<string>();
    public List<string> ChronicDiseases { get; set; } = new List<string>();
    public List<MedicalRecordModel> MedicalRecords { get; set; } = new List<MedicalRecordModel>();
}



