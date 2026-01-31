using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemType { get; set; } = null!;

    public string UnitType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public virtual ICollection<BrigadeItem> BrigadeItems { get; set; } = new List<BrigadeItem>();
}
