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

    public string? Phone { get; set; }
    public int UrgencyType { get; set; }
    public string Address { get; set; } = null!;

    public DateTime StartCallTime { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public List<BrigadeDto>? AssignedBrigades { get; set; }


    // додаткові поля лише для відображення
    public string? PatientFullName { get; set; }
    public string? DispatcherIndentificator{ get; set; }
    public string? HospitalName { get; set; }

    public TimeSpan? EstimatedArrival { get; set; }
}

