using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmbulanceSystem.Core.Entities.Types;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class BrigadeMember
{
    [Key]
    [Column("BrigadeMemberID")]
    public int BrigadeMemberId { get; set; }

    [Column("PersonID")]
    public int PersonId { get; set; }

    [Column("BrigadeID")]
    public int BrigadeId { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [Column("MemberSpecializationTypeID")]
    public int MemberSpecializationTypeId { get; set; }

    [ForeignKey("BrigadeId")]
    [InverseProperty("BrigadeMembers")]
    public virtual Brigade Brigade { get; set; } = null!;

    [InverseProperty("BrigadeMember")]
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    [ForeignKey("MemberSpecializationTypeId")]
    [InverseProperty("BrigadeMembers")]
    public virtual MemberSpecializationType MemberSpecializationType { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("BrigadeMembers")]
    public virtual Person Person { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("BrigadeMembers")]
    public virtual BrigadeMemberRole Role { get; set; } = null!;
}
