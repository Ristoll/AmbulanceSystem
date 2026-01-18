using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Ambulance.Core.Entities;

[Table("Person")]
public partial class Person
{
    [Key]
    [Column("PersonID")]
    public int PersonId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Surname { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MiddleName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Login { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Gender { get; set; }

    [Unicode(false)]
    public string? PasswordHash { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    public Geometry? Address { get; set; }

    [Column("Image_Url")]
    [StringLength(200)]
    [Unicode(false)]
    public string? ImageUrl { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string UserRole { get; set; } = null!;

    [InverseProperty("Person")]
    public virtual ICollection<BrigadeMemberRole> BrigadeMemberRoles { get; set; } = new List<BrigadeMemberRole>();

    [InverseProperty("Person")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();

    [InverseProperty("Dispatcher")]
    public virtual ICollection<Call> CallDispatchers { get; set; } = new List<Call>();

    [InverseProperty("Patient")]
    public virtual ICollection<Call> CallPatients { get; set; } = new List<Call>();

    [InverseProperty("Person")]
    public virtual MedicalCard? MedicalCard { get; set; }
}
