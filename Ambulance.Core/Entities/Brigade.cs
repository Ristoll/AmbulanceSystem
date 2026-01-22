using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("Brigade")]
public partial class Brigade
{
    [Key]
    [Column("brigade_id")]
    public int BrigadeId { get; set; }

    [Column("current_call_id")]
    public int? CurrentCallId { get; set; }

    [Column("brigade_state")]
    [StringLength(50)]
    public string BrigadeState { get; set; } = null!;
    
    [Column("brigade_type")]
    [StringLength(50)]
    public string BrigadeType { get; set; } = null!;

    [InverseProperty("brigade")]
    public virtual ICollection<BrigadeItem> BrigadeItems { get; set; } = new List<BrigadeItem>();

    [InverseProperty("brigade")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();
}
