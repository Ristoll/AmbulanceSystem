using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class Dispatcher
{
    [Key]
    [Column("DispatcherID")]
    public int DispatcherId { get; set; }

    [Column("PersonID")]
    public int PersonId { get; set; }

    [InverseProperty("Dispatcher")]
    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();

    [ForeignKey("PersonId")]
    [InverseProperty("Dispatchers")]
    public virtual Person Person { get; set; } = null!;
}
