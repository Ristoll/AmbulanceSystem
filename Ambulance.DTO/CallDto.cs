using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.DTO;

public class CallDto
{
    public int CallId { get; set; }
    public int? PatientId { get; set; }
    public int DispatcherId { get; set; }

    public string? Phone { get; set; } = string.Empty;
    public string UrgencyType { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Notes { get; set; } = string.Empty;

    public DateTime StartCallTime { get; set; } = DateTime.UtcNow;

    public List<BrigadeDto>? AssignedBrigades { get; set; }


    // додаткові поля лише для відображення
    public string? PatientFullName { get; set; }
    public string? DispatcherIndentificator{ get; set; }
    public string? HospitalName { get; set; }

    public TimeSpan? EstimatedArrival { get; set; }
}

