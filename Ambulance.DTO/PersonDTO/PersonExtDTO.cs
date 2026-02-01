namespace Ambulance.DTO.PersonModels;

public class PersonExtDTO : PersonBase
{
    public string Login { get; set; } = null!;
    public int RoleLevel { get; set; } = -1; // -1 це undefined рівень ролі
}
