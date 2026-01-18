namespace Ambulance.DTO.PersonModels;

public abstract class PersonBase
{
    public int? PersonId { get; set; }
    public string? Name { get; set; } = null!;
    public string? Surname { get; set; } = null!;
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
