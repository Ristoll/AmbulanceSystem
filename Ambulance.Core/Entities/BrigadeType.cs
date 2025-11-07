using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class BrigadeType
{
    [Key]
    [Column("BrigadeTypeID")]
    public int BrigadeTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("BrigadeType")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();
}
