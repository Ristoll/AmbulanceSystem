﻿using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.BLL.Models;

public class CallModel
{
    public int CallId { get; set; }
    public int PatientId { get; set; }
    public int DispatcherId { get; set; }
    public int? HospitalId { get; set; }
    public int? CallStatusId { get; set; }
    public string? Phone { get; set; }
    public int UrgencyType { get; set; }
    public Point? Address { get; set; }
    public DateTime? StartCallTime { get; set; }
    public DateTime? EndCallTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public DateTime? CompletionTime { get; set; }
    public string? CallerName { get; set; }
    public string? PatientName { get; set; }
    public string? DispatcherName { get; set; }
    public string? HospitalName { get; set; }
    public string? Notes { get; set; }
    public TimeSpan EstimatedArrival { get; set; }
}

