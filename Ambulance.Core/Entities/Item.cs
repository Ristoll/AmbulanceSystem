using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("Item")]
public partial class Item
{
    [Key]
    [Column("item_id")]
    public int ItemId { get; set; }

    [Column("item_type")]
    [StringLength(50)]
    public string ItemType { get; set; } = null!;

    [Column("unit_type")]
    [StringLength(50)]
    public string UnitType { get; set; } = null!;
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("image_url")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [InverseProperty("item")]
    public virtual ICollection<BrigadeItem> BrigadeItems { get; set; } = new List<BrigadeItem>();
}
