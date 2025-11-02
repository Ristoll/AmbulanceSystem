using Ambulance.Core.Entities;

namespace Ambulance.BLL.Models;

public class PersonCreateModel
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!; // чисто для передачі при створенні
    public string? ImageUrl { get; set; }

    public int RoleId { get; set; } // надсилаємо тільки Id
}
