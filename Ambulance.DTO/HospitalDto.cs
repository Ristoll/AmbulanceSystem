using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AmbulanceSystem.DTO
{
    public class HospitalDto
    {
        public int HospitalId { get; set; }
        public string? Name { get; set; }
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public List<BrigadeDto>? Brigades { get; set; } = new List<BrigadeDto>();
        public List<CallDto>? Calls { get; set; }
    }
}
