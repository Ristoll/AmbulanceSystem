using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("MedicalCard")]
public partial class MedicalCard
{
    [Key]
    [Column("card_id")]
    public int CardId { get; set; }
    [Column("creation_date")]
    public DateOnly CreationDate { get; set; }

    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }
    [Column("blood_type")]
    [StringLength(10)]
    public string? BloodType { get; set; }

    [Column("height")]
    public int? Height { get; set; }

    [Column("weight")]
    public int? Weight { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [InverseProperty("card")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [InverseProperty("card")]
    public virtual ICollection<PatientAllergy> PatientAllergies { get; set; } = new List<PatientAllergy>();

    [InverseProperty("card")]
    public virtual ICollection<PatientChronicDecease> PatientChronicDeceases { get; set; } = new List<PatientChronicDecease>();

    [InverseProperty("card")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
