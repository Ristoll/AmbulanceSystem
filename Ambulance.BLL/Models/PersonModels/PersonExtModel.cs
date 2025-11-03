using Ambulance.Core.Entities;

namespace Ambulance.BLL.Models.PersonModels;

public class PersonExtModel : PersonBaseModel
{
    public int PersonId { get; set; }
    public string Login { get; set; } = null!;
    public UserRoleViewModel Role { get; set; } = null!;
}