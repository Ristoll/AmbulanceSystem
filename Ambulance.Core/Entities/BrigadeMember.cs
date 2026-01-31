using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class BrigadeMember
{
    public int BrigadeMemberId { get; set; }

    public int PersonId { get; set; }

    public int BrigadeId { get; set; }

    public int SpecializationTypeId { get; set; }

    public virtual Brigade Brigade { get; set; } = null!;

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Person Person { get; set; } = null!;

    public virtual SpecializationType SpecializationType { get; set; } = null!;
}
