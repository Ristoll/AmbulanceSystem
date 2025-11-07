using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class PatientAllergy
{
    [Key]
    [Column("PatientAllergyID")]
    public int PatientAllergyId { get; set; }

    [Column("MedicalCardID")]
    public int MedicalCardId { get; set; }

    [Column("AllergyID")]
    public int AllergyId { get; set; }

    [ForeignKey("AllergyId")]
    [InverseProperty("PatientAllergies")]
    public virtual Allergy Allergy { get; set; } = null!;

    [ForeignKey("MedicalCardId")]
    [InverseProperty("PatientAllergies")]
    public virtual MedicalCard MedicalCard { get; set; } = null!;
}
