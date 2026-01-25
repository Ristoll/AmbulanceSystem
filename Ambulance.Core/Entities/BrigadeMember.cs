using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("BrigadeMember")]
public partial class BrigadeMember
{
    [Key]
    [Column("brigade_member_id")]
    public int BrigadeMemberId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("brigade_id")]
    public int BrigadeId { get; set; }

    [Column("specialization_type_id")]
    public int SpecializationTypeId { get; set; }

    [Column("role_name")]
    public string RoleName { get; set; } = string.Empty;

    [InverseProperty("brigade_member")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [ForeignKey("brigade_id")]
    [InverseProperty("BrigadeMembers")]
    public virtual Brigade Brigade { get; set; } = null!;

    [ForeignKey("person_id")]
    [InverseProperty("BrigadeMembers")]
    public virtual Person Person { get; set; } = null!;
    [ForeignKey("specialization_type_id")]
    [InverseProperty("BrigadeMembers")]
    public virtual SpecializationType SpecializationType { get; set; } = null!;
}
