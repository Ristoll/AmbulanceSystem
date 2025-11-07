namespace Ambulance.DTO.PersonModels;

public class PersonProfileDTO
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Login { get; set; } = null!;
    public string? ImageUrl { get; set; }
}
