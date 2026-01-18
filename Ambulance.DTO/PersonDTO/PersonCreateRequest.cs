namespace Ambulance.DTO.PersonModels;

public class PersonCreateRequest : PersonBase
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!; // для передачі при створенні
    public string? Role { get; set; }
}
