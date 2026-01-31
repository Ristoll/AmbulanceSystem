using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class PatientAllergy
{
    public int PatientAllergyId { get; set; }

    public int CardId { get; set; }

    public int AllergyId { get; set; }

    public virtual Allergy Allergy { get; set; } = null!;

    public virtual MedicalCard Card { get; set; } = null!;
}
