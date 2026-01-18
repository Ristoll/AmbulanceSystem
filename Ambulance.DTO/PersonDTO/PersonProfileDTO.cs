namespace Ambulance.DTO.PersonModels;

public class PersonProfileDTO
{
    public int PersonId { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Login { get; set; } = null!;
    public ImageDto? Image { get; set; }
}
