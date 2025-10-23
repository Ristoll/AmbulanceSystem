namespace AmbulanceSystem.BLL.Models;

public class ActionLogModel
{
    public int ActionLogId { get; set; }
    public int PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Action { get; set; }
}
