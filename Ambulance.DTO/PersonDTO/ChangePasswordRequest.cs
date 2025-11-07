namespace Ambulance.DTO.PersonModels;

public class ChangePasswordRequest
{
    public int PersonID { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
