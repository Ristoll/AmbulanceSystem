using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class MedicalCard
{
    public int CardId { get; set; }

    public DateOnly CreationDate { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? BloodType { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public string? Notes { get; set; }

    public int PatientId { get; set; }

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual Person Patient { get; set; } = null!;

    public virtual ICollection<PatientAllergy> PatientAllergies { get; set; } = new List<PatientAllergy>();

    public virtual ICollection<PatientChronicDecease> PatientChronicDeceases { get; set; } = new List<PatientChronicDecease>();
}
