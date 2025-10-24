using AmbulanceSystem.Core.Entities;

namespace AmbulanceSystem.BLL.Models;

public class PersonModel
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Login { get; set; }
    public string PasswordHash { get; set; }
    public string? ImageUrl { get; set; }

    public UserRole Role { get; set; } = UserRole.Unknown;
}
