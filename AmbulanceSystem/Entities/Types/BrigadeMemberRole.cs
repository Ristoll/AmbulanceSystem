using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities.Types;

[Table("BrigadeMemberRole")]
public partial class BrigadeMemberRole
{
    [Key]
    [Column("RoleID")]
    public int RoleId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();
}
