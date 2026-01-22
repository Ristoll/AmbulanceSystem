using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("SpecializationType")]
[Index("name", Name = "UQ__Speciali__72E12F1B6C4E6858", IsUnique = true)]
public partial class SpecializationType
{
    [Key]
    [Column("specialization_type_id")]
    public int SpecializationTypeId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("specialization_type")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();
}
