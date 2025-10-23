using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class ActionLog
{
    [Key]
    [Column("ActionLogLogID")]
    public int ActionLogLogId { get; set; }

    [Column("PersonID")]
    public int PersonId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Action { get; set; }

    [ForeignKey("PersonId")]
    [InverseProperty("Logs")]
    public virtual Person Person { get; set; } = null!;

    public ActionLog(string action, Person actionOwner)
    {
        this.Action = action;
        this.Person = actionOwner;
    }
}
