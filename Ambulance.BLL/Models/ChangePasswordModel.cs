namespace Ambulance.BLL.Models;

public class ChangePasswordModel
{
    public int PersonID { get; set; }
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
