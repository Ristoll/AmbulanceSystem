using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities.Types;

public partial class MemberSpecializationType
{
    [Key]
    [Column("SpecializationTypeID")]
    public int SpecializationTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("MemberSpecializationType")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();

    /*Basic = 1,
    Resuscitation,
    Pediatric,
    Obstetric,
    Psychiatric,
    Toxicology*/
}
