namespace Ambulance.DTO.PersonModels;

public class AuthResponse
{
    public string JwtToken { get; set; } = null!;

    public string Login { get; set; } = null!;
    public string UserRole { get; set; } = null!;
}
