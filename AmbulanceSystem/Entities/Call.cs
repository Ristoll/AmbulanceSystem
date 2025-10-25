using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace AmbulanceSystem.Core.Entities;

public partial class Call
{
    [Key]
    [Column("CallID")]
    public int CallId { get; set; }

    [Column("CallerID")]
    public int CallerId { get; set; }

    [Column("PatientID")]
    public int PatientId { get; set; }

    [Column("DispatcherID")]
    public int DispatcherId { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    [Column("StatusID")]
    public int? CallStatusId { get; set; }

    public int UrgencyType { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public Point? Address { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartCallTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndCallTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ArrivalTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CompletionTime { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Notes { get; set; }

    [InverseProperty("CurrentCall")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();

    [ForeignKey("CallerId")]
    [InverseProperty("Calls")]
    public virtual Person Caller { get; set; } = null!;

    [ForeignKey("DispatcherId")]
    [InverseProperty("Calls")]
    public virtual Dispatcher Dispatcher { get; set; } = null!;

    [ForeignKey("HospitalId")]
    [InverseProperty("Calls")]
    public virtual Hospital? Hospital { get; set; }

    [ForeignKey("PatientId")]
    [InverseProperty("Calls")]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey("CallStatusId")]
    [InverseProperty("Calls")]
    public virtual CallStatus CallStatus { get; set; } = null!;
}
