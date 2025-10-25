namespace Ambulance.BLL.Models;

public class AuthResponseModel
{
    public string JwtToken { get; set; } = null!;

    public int PersonId { get; set; }
    public string Login { get; set; } = null!;
    public string Role { get; set; } = null!;
}
