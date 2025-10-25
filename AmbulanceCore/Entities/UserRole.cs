using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

public partial class UserRole
{
    [Key]
    public int UserRoleId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [InverseProperty("UserRole")]
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
