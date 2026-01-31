using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class ChronicDecease
{
    public int ChronicDeceaseId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PatientChronicDecease> PatientChronicDeceases { get; set; } = new List<PatientChronicDecease>();
}
