using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

public partial class PatientChronicDecease
{
    [Key]
    [Column("patient_chronic_decease_id")]
    public int PatientChronicDeceaseId { get; set; }

    [Column("card_id")]
    public int CardId { get; set; }

    [Column("chronic_decease_id")]
    public int ChronicDeceaseId { get; set; }

    [ForeignKey("card_id")]
    [InverseProperty("PatientChronicDeceases")]
    public virtual MedicalCard Card { get; set; } = null!;

    [ForeignKey("chronic_decease_id")]
    [InverseProperty("PatientChronicDeceases")]
    public virtual ChronicDecease ChronicDecease { get; set; } = null!;
}
