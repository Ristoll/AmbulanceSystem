namespace Ambulance.DTO.PersonModels;

public abstract class PersonBase
{
    public int? PersonId { get; set; }
    public string? Name { get; set; } = null!;
    public string? Surname { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public int Gender { get; set; } = -1; // -1 це undefined стать
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
