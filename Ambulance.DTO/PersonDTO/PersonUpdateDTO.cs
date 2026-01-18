namespace Ambulance.DTO.PersonModels;

// надсилання тільки змінених полів перекладається на фронт
public class PersonUpdateDTO : PersonBase
{
    public string? Role { get; set; }
    public string? Login { get; set; }
    public ImageDto? Image { get; set; }
}
