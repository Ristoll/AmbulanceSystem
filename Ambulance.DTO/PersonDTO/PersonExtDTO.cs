namespace Ambulance.DTO.PersonModels;

public class PersonExtDTO : PersonBase
{
    public int PersonId { get; set; }
    public string Login { get; set; } = null!;
    public string Role { get; set; } = null!;
}
