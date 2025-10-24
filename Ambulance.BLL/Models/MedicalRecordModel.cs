using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.BLL.Models;
public class MedicalRecordModel
{
    public int MedicalRecordId { get; set; }
    public int MedicalCardId { get; set; }
    public int BrigadeMemberId { get; set; }
    public DateTime? CreationDate { get; set; }
    public string? Diagnoses { get; set; }
    public string? Symptoms { get; set; }
    public string? Treatment { get; set; }
    public string? ImageUrl { get; set; }
    public string? BrigadeMemberName { get; set; }
    public string? MedicalCardPatientName { get; set; }
}
