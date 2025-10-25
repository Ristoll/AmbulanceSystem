using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class MedicalCard
{
    [Key]
    [Column("MedicalCardID")]
    public int MedicalCardId { get; set; }

    [Column("PersonID")]
    public int PersonId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreationDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? BloodType { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Height { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Weight { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [InverseProperty("MedicalCard")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [InverseProperty("MedicalCard")]
    public virtual ICollection<PatientAllergy> PatientAllergies { get; set; } = new List<PatientAllergy>();

    [InverseProperty("MedicalCard")]
    public virtual ICollection<PatientChronicDecease> PatientChronicDeceases { get; set; } = new List<PatientChronicDecease>();

    [ForeignKey("PersonId")]
    [InverseProperty("MedicalCards")]
    public virtual Person Person { get; set; } = null!;
}
