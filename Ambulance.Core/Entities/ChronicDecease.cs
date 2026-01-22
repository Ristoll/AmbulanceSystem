using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Index("name", Name = "UQ__ChronicD__72E12F1B3666CB21", IsUnique = true)]
public partial class ChronicDecease
{
    [Key]
    [Column("chronic_decease_id")]
    public int ChronicDeceaseId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("chronic_decease")]
    public virtual ICollection<PatientChronicDecease> PatientChronicDeceases { get; set; } = new List<PatientChronicDecease>();
}
