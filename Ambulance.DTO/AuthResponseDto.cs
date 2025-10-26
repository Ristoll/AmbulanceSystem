namespace Ambulance.DTO;

public class AuthResponseDto
{
    public string JwtToken { get; set; } = null!;

    public string Login { get; set; } = null!;
    public string UserRole { get; set; } = null!;
}
