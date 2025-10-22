using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.BLL.Models;

public class LogModel
{
    public int LogId { get; set; }
    public int PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Action { get; set; }
}
