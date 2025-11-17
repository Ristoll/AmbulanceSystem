using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

public partial class Hospital
{
    [Key]
    [Column("HospitalID")]
    public int HospitalId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Location { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Note { get; set; }

    [InverseProperty("Hospital")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();

    [InverseProperty("Hospital")]
    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();
}
