using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

public partial class Call
{
    [Key]
    [Column("CallID")]
    public int CallId { get; set; }

    [Column("PatientID")]
    public int? PatientId { get; set; }

    [Column("DispatcherID")]
    public int? DispatcherId { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartCallTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndCallTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ArrivalTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CompletionTime { get; set; }

    [Unicode(false)]
    public string? Notes { get; set; }

    public int UrgencyType { get; set; }

    [InverseProperty("CurrentCall")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();

    [ForeignKey("DispatcherId")]
    [InverseProperty("CallDispatchers")]
    public virtual Person Dispatcher { get; set; } = null!;

    [ForeignKey("HospitalId")]
    [InverseProperty("Calls")]
    public virtual Hospital? Hospital { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("CallPatients")]
    public virtual Person Patient { get; set; } = null!;
}
