using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.DTO;

public class BrigadeItemDto
{
    public int BrigadeItemId { get; set; }
    public int BrigadeId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; } = 0;
    public DateOnly? ExpiryDate { get; set; }
    public string? ItemType { get; set; } = string.Empty;
    public string? BrigadeName { get; set; } = string.Empty;
    public string? ItemName { get; set; } = string.Empty;
    public string? UnitType { get; set; } = string.Empty;
}
