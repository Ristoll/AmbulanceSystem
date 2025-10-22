using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities.Types;

public partial class UnitType
{
    [Key]
    [Column("UnitTypeID")]
    public int UnitTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [InverseProperty("UnitType")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
