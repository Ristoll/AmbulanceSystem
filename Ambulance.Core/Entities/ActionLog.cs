using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class ActionLog
{
    public ActionLog(string actionDescription, int actionOwner)
    {
        Action = actionDescription;
        PersonId = actionOwner;
    }

    [Key]
    [Column("ActionLogID")]
    public int ActionLogId { get; set; }

    [Column("PersonID")]
    public int PersonId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Action { get; set; }

    [ForeignKey("PersonId")]
    [InverseProperty("ActionLogs")]
    public virtual Person Person { get; set; } = null!;
}
