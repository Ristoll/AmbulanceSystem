﻿namespace AmbulanceSystem.DTO;

public class PersonCreateDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Login { get; set; }
    public string? password { get; set; } // чисто для передачі при створенні
    public string? ImageUrl { get; set; }
}
