using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

public partial class BrigadeState
{
    [Key]
    [Column("BrigadeStateID")]
    public int BrigadeStateId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("BrigadeState")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();
}
