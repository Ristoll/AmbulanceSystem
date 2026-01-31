using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class Call
{
    public int CallId { get; set; }

    public int? MedicalRecordId { get; set; }

    public string UrgencyType { get; set; } = null!;

    public string CallAddress { get; set; } = null!;

    public string CallState { get; set; } = null!;

    public DateTime CallAt { get; set; }

    public int? PersonId { get; set; }

    public int? HospitalId { get; set; }

    public int? DispatcherId { get; set; }

    public virtual Person? Dispatcher { get; set; }

    public virtual Hospital? Hospital { get; set; }

    public virtual MedicalRecord? MedicalRecord { get; set; }

    public virtual Person? Person { get; set; }
}
