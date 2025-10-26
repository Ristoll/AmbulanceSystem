namespace Ambulance.BLL.Models;

public class AuthResponseModel
{
    public string JwtToken { get; set; } = null!;

    public string Login { get; set; } = null!;
    public string UserRole { get; set; } = null!;
}
