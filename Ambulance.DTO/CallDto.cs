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
    public int? HospitalId { get; set; }

    public string? Phone { get; set; }
    public int UrgencyType { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }

    public DateTime? StartCallTime { get; set; }
    public DateTime? EndCallTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public DateTime? CompletionTime { get; set; }

    public List<BrigadeDto>? AssignedBrigades { get; set; }


    // додаткові поля лише для відображення
    public string? PatientFullName { get; set; }
    public string? DispatcherIndentificator{ get; set; }
    public string? HospitalName { get; set; }

    public TimeSpan? EstimatedArrival { get; set; }
}

