namespace Ambulance.DTO;

public class ChangePasswordDTO
{
    public int PersonID { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
