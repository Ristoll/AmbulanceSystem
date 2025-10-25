﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.Core.Entities;

public partial class Brigade
{
    [Key]
    [Column("BrigadeID")]
    public int BrigadeId { get; set; }

    [Column("BrigadeStateID")]
    public int BrigadeStateId { get; set; }

    [Column("BrigadeTypeID")]
    public int BrigadeTypeId { get; set; }

    [Column("HospitalID")]
    public int? HospitalId { get; set; }

    [Column("CurrentCallID")]
    public int? CurrentCallId { get; set; }

    [InverseProperty("Brigade")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();

    [ForeignKey("BrigadeStateId")]
    [InverseProperty("Brigades")]
    public virtual BrigadeState BrigadeState { get; set; } = null!;

    [ForeignKey("BrigadeTypeId")]
    [InverseProperty("Brigades")]
    public virtual BrigadeType BrigadeType { get; set; } = null!;

    [ForeignKey("CurrentCallId")]
    [InverseProperty("Brigades")]
    public virtual Call? CurrentCall { get; set; }

    [ForeignKey("HospitalId")]
    [InverseProperty("Brigades")]
    public virtual Hospital? Hospital { get; set; }
}
