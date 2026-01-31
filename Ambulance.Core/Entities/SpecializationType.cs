using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class SpecializationType
{
    public int SpecializationTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();
}
