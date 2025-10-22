using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class PatientChronicDecease
{
    [Key]
    [Column("PatientChronicDeceaseID")]
    public int PatientChronicDeceaseId { get; set; }

    [Column("MedicalCardID")]
    public int MedicalCardId { get; set; }

    [Column("ChronicDeceaseID")]
    public int ChronicDeceaseId { get; set; }

    [ForeignKey("ChronicDeceaseId")]
    [InverseProperty("PatientChronicDeceases")]
    public virtual ChronicDecease ChronicDecease { get; set; } = null!;

    [ForeignKey("MedicalCardId")]
    [InverseProperty("PatientChronicDeceases")]
    public virtual MedicalCard MedicalCard { get; set; } = null!;
}
