using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class ChronicDecease
{
    [Key]
    [Column("ChronicDeceaseID")]
    public int ChronicDeceaseId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("ChronicDecease")]
    public virtual ICollection<PatientChronicDecease> PatientChronicDeceases { get; set; } = new List<PatientChronicDecease>();
}
