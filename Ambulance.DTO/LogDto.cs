using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.DTO;

public class LogDto
{
    public int LogId { get; set; }
    public int PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Action { get; set; }
}
