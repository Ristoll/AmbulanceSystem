using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.DTO;

public class BrigadeTypeDto
{
    public int BrigadeTypeId { get; set; }
    public string Name { get; set; } = null!;
}
