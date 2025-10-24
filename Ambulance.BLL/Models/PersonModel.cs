using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.BLL.Models;

public class PersonModel
{
    public int PersonId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? ImageUrl { get; set; }
}

