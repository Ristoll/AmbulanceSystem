using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmbulanceSystem.Core.Entities.Types;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class Item
{
    [Key]
    [Column("ItemID")]
    public int ItemId { get; set; }

    [Column("ItemTypeID")]
    public int ItemTypeId { get; set; }

    [Column("UnitTypeID")]
    public int UnitTypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("Image_Url")]
    [StringLength(200)]
    [Unicode(false)]
    public string? ImageUrl { get; set; }

    [ForeignKey("ItemTypeId")]
    [InverseProperty("Items")]
    public virtual ItemType ItemType { get; set; } = null!;

    [ForeignKey("UnitTypeId")]
    [InverseProperty("Items")]
    public virtual UnitType UnitType { get; set; } = null!;
}
