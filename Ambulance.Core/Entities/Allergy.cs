using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class Allergy
{
    [Key]
    [Column("AllergyID")]
    public int AllergyId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [InverseProperty("Allergy")]
    public virtual ICollection<PatientAllergy> PatientAllergies { get; set; } = new List<PatientAllergy>();
}
