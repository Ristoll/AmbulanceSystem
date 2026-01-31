using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class BrigadeItem
{
    public int BrigadeItemId { get; set; }

    public int ItemId { get; set; }

    public int BrigadeId { get; set; }

    public int Quantity { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual Brigade Brigade { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
