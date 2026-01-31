using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class PatientChronicDecease
{
    public int PatientChronicDeceaseId { get; set; }

    public int CardId { get; set; }

    public int ChronicDeceaseId { get; set; }

    public virtual MedicalCard Card { get; set; } = null!;

    public virtual ChronicDecease ChronicDecease { get; set; } = null!;
}
