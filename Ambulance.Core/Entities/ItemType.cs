using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

public partial class ItemType
{
    [Key]
    [Column("ItemTypeID")]
    public int ItemTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [InverseProperty("ItemType")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
