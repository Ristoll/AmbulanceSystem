namespace Ambulance.Core.Entities;

public partial class Hospital
{
    public int HospitalId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();

    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();
}
