using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Keyless]
public partial class BrigadeItem
{
    [Column("BrigadeItemID")]
    public int BrigadeItemId { get; set; }

    [Column("BrigadeID")]
    public int BrigadeId { get; set; }

    [Column("ItemID")]
    public int ItemId { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    [ForeignKey("BrigadeId")]
    public virtual Brigade Brigade { get; set; } = null!;

    [ForeignKey("ItemId")]
    public virtual Item Item { get; set; } = null!;
}
