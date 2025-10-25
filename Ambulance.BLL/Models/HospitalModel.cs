using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
namespace AmbulanceSystem.BLL.Models;

public class HospitalModel
{
    public int HospitalId { get; set; }
    public string? Name { get; set; }
    public Point? Location { get; set; }
    public string? Note { get; set; }
    public List<BrigadeModel>? Brigades { get; set; } = new List<BrigadeModel>();
    public List<CallModel>? Calls { get; set; }
}
