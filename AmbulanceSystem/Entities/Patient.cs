using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class Patient
{
    [Key]
    [Column("PatientID")]
    public int PatientId { get; set; }

    [Column("PersonID")]
    public int PersonId { get; set; }

    [Column("MedicalCardID")]
    public int MedicalCardId { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();

    [ForeignKey("PersonId")]
    [InverseProperty("Patient")]
    public virtual Person Person { get; set; } = null!;

    [ForeignKey("MedicalCardId")]
    [InverseProperty("Patient")]
    public virtual MedicalCard MedicalCard { get; set; } = null!;
}
