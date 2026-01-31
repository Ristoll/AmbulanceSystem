namespace Ambulance.DTO;

public class UrgencyTypeDto // запитати Крістіну щодо можливої оптимізації під enum
{
    public int Level { get; set; }
    public string Name { get; set; } = null!;
}
