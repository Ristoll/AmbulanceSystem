using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

public partial class PatientAllergy
{
    [Key]
    [Column("patient_allergy_id")]
    public int PatientAllergyId { get; set; }

    [Column("card_id")]
    public int CardId { get; set; }

    [Column("allergy_id")]
    public int AllergyId { get; set; }

    [ForeignKey("allergy_id")]
    [InverseProperty("PatientAllergies")]
    public virtual Allergy Allergy { get; set; } = null!;

    [ForeignKey("card_id")]
    [InverseProperty("PatientAllergies")]
    public virtual MedicalCard Card { get; set; } = null!;
}
