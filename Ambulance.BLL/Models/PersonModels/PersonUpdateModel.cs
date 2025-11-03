namespace Ambulance.BLL.Models.PersonModels;

// надсилання тільки змінених полів перекладається на фронт
public class PersonUpdateModel : PersonBaseModel
{
    public int PersonId { get; set; }
    public int? RoleId { get; set; }
}
