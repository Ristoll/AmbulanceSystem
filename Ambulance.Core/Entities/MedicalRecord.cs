using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class MedicalRecord
{
    [Key]
    [Column("MedicalRecordID")]
    public int MedicalRecordId { get; set; }

    [Column("MedicalCardID")]
    public int MedicalCardId { get; set; }

    [Column("BrigadeMemberID")]
    public int BrigadeMemberId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DataTime { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Diagnoses { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Symptoms { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Treatment { get; set; }

    [Column("Image_Url")]
    [StringLength(200)]
    [Unicode(false)]
    public string? ImageUrl { get; set; }

    [ForeignKey("BrigadeMemberId")]
    [InverseProperty("MedicalRecords")]
    public virtual BrigadeMember BrigadeMember { get; set; } = null!;

    [ForeignKey("MedicalCardId")]
    [InverseProperty("MedicalRecords")]
    public virtual MedicalCard MedicalCard { get; set; } = null!;
}
