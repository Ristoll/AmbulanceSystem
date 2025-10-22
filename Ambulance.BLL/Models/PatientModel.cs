using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.BLL.Models;

public class PatientModel
{
    public int PatientId { get; set; }
    public int PersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string SecondName { get; set; } = string.Empty;
    public List<string> Allergies { get; set; } = new List<string>();
    public List<string> ChronicDiseases { get; set; } = new List<string>();
    public List<CallModel> Calls { get; set; } = new List<CallModel>();
    public List<MedicalCardModel> MedicalCards { get; set; } = new List<MedicalCardModel>();
}

