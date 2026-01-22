using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Index("name", Name = "UQ__Allergie__72E12F1B8D47EE21", IsUnique = true)]
public partial class Allergy
{
    [Key]
    [Column("allergy_id")]
    public int AllergyId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("allergy")]
    public virtual ICollection<PatientAllergy> PatientAllergies { get; set; } = new List<PatientAllergy>();
}
