using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace AmbulanceSystem.Core.Entities;

public partial class Person
{
    [Key]
    [Column("PersonID")]
    public int PersonId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Surname { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? MiddleName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    public Point? Address { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    [Column("Image_Url")]
    [StringLength(200)]
    [Unicode(false)]
    public string? ImageUrl { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();

    [InverseProperty("Caller")]
    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();

    [InverseProperty("Person")]
    public virtual ICollection<Dispatcher> Dispatchers { get; set; } = new List<Dispatcher>();

    [InverseProperty("Person")]
    public virtual ICollection<ActionLog> Logs { get; set; } = new List<ActionLog>();

    [InverseProperty("Person")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
