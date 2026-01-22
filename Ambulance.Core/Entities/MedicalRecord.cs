using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("MedicalRecord")]
public partial class MedicalRecord
{
    [Key]
    [Column("record_id")]
    public int RecordId { get; set; }

    [Column("card_id")]
    public int CardId { get; set; }

    [Column("brigade_member_id")]
    public int BrigadeMemberId { get; set; }

    [Column("date_time")]
    public DateTime DateTime { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("image_url")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }
    
    [InverseProperty("medical_record")]
    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();

    [ForeignKey("brigade_member_id")]
    [InverseProperty("MedicalRecords")]
    public virtual BrigadeMember BrigadeMember { get; set; } = null!;

    [ForeignKey("card_id")]
    [InverseProperty("MedicalRecords")]
    public virtual MedicalCard Card { get; set; } = null!;
}
