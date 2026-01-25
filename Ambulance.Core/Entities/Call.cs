using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("Call")]
public partial class Call
{
    [Key]
    [Column("call_id")]
    public int CallId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("dispatcher_id")]
    public int DispatcherId { get; set; }

    [Column("medical_record_id")]
    public int? MedicalRecordId { get; set; }

    [Column("urgency_type")]
    [StringLength(50)]
    public string UrgencyType { get; set; } = null!;

    [Column("call_address")]
    [StringLength(255)]
    public string CallAddress { get; set; } = null!;

    [Column("destination_address")]
    [StringLength(255)]
    public string? DestinationAddress { get; set; }

    [Column("call_state")]
    [StringLength(50)]
    public string CallState { get; set; } = null!;

    [Column("call_at", TypeName = "datetime2")]
    public DateTime CallAt { get; set; } = DateTime.UtcNow;

    [Column("notes")]
    public string? Notes { get; set; } = string.Empty;

    [Column("phone_number")]
    [StringLength(13)]
    public string? PhoneNumber { get; set; } = string.Empty;

    [ForeignKey("dispatcher_id")]
    [InverseProperty("Calldispatchers")]
    public virtual Person Dispatcher { get; set; } = null!;

    [ForeignKey("medical_record_id")]
    [InverseProperty("Calls")]
    public virtual MedicalRecord? MedicalRecord { get; set; }
    [ForeignKey("person_id")]
    [InverseProperty("Callpeople")]
    public virtual Person Person { get; set; } = null!;
}
