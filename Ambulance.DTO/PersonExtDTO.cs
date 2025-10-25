namespace Ambulance.DTO;

public class PersonExtDTO
{
    public int PersonId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Login { get; set; }
    public string? ImageUrl { get; set; }

    public string Role { get; set; } = null!;
}
