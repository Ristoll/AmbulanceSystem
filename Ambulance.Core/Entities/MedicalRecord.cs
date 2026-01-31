using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int CardId { get; set; }

    public int BrigadeMemberId { get; set; }

    public DateTime DateTime { get; set; }

    public string? Notes { get; set; }

    public string? ImageUrl { get; set; }

    public virtual BrigadeMember BrigadeMember { get; set; } = null!;

    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();

    public virtual MedicalCard Card { get; set; } = null!;
}
