using System;
using System.Collections.Generic;
using System.Drawing;

namespace AmbulanceSystem.BLL.Models;

public class HospitalModel
{
    public int HospitalId { get; set; }
    public string? Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Note { get; set; }
    public List<BrigadeModel>? Brigades { get; set; } = new List<BrigadeModel>();
    public List<CallModel>? Calls { get; set; }
}
