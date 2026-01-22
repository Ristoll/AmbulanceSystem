using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("BrigadeItem")]
public partial class BrigadeItem
{
    [Key]
    [Column("brigade_item_id")]
    public int BrigadeItemId { get; set; }

    [Column("item_id")]
    public int ItemId { get; set; }

    [Column("brigade_id")]
    public int BrigadeId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("expiry_date")]
    public DateOnly? ExpiryDate { get; set; }

    [ForeignKey("brigade_id")]
    [InverseProperty("BrigadeItems")]
    public virtual Brigade Brigade { get; set; } = null!;

    [ForeignKey("item_id")]
    [InverseProperty("BrigadeItems")]
    public virtual Item Item { get; set; } = null!;
}
