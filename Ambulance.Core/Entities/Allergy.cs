using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class Allergy
{
    public int AllergyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PatientAllergy> PatientAllergies { get; set; } = new List<PatientAllergy>();
}
