namespace Ambulance.BLL.Models;

// надсилання тільки змінених полів перекладається на фронт
public class PersonUpdateModel
{
    public int PersonId { get; set; } // якщо адмін = айді з PersonExtModel, якщо юзер = свій власний айді з токену
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public int? RoleId { get; set; } // лише для адміністратора
}
