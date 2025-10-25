using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("BrigadeMemberRole")]
public partial class BrigadeMemberRole
{
    [Key]
    [Column("BrigadeMemberRoleID")]
    public int BrigadeMemberRoleId { get; set; }

    [Column("PersonID")]
    public int? PersonId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("BrigadeMemberRole")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();

    [ForeignKey("PersonId")]
    [InverseProperty("BrigadeMemberRoles")]
    public virtual Person? Person { get; set; }
}
