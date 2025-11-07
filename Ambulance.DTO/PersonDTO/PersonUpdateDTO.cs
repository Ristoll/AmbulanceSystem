namespace Ambulance.DTO.PersonModels;

// надсилання тільки змінених полів перекладається на фронт
public class PersonUpdateDTO : PersonBase
{
    public int PersonId { get; set; }
    public int? RoleId { get; set; }
}
