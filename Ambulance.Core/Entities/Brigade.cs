using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class Brigade
{
    public int BrigadeId { get; set; }

    public int? CurrentCallId { get; set; }

    public string BrigadeState { get; set; } = null!;

    public string BrigadeType { get; set; } = null!;

    public int? HospitalId { get; set; }

    public virtual ICollection<BrigadeItem> BrigadeItems { get; set; } = new List<BrigadeItem>();

    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();

    public virtual Hospital? Hospital { get; set; }
}
