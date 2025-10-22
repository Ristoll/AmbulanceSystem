using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class UsedItem
{
    [Key]
    [Column("UsedItemID")]
    public int UsedItemId { get; set; }

    [Column("ItemID")]
    public int ItemId { get; set; }

    [Column("CallID")]
    public int CallId { get; set; }

    [Column("MedicalRecordID")]
    public int MedicalRecordId { get; set; }

    [Column("BrigadeID")]
    public int BrigadeId { get; set; }

    public int Quantity { get; set; }
}
