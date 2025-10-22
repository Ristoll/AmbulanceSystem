using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.DTO;

public class DispatcherDto
{
    public int DispatcherId { get; set; }
    public int PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public int CallCount { get; set; }
}
