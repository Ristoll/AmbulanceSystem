namespace Ambulance.BLL.Models.PersonModels;

public class PersonCreateModel : PersonBaseModel
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!; // для передачі при створенні
    public int RoleId { get; set; }
}
