using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

[Table("Person")]
[Index("login", Name = "UQ__Person__7838F272A5D00C77", IsUnique = true)]
[Index("email", Name = "UQ__Person__AB6E616454B41C00", IsUnique = true)]
public partial class Person
{
    [Key]
    [Column("person_id")]
    public int PersonId { get; set; }

    [Column("card_id")]
    public int? CardId { get; set; }

    [Column("login")]
    [StringLength(50)]
    public string Login { get; set; } = null!;

    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("user_role")]
    [StringLength(50)]
    public string UserRole { get; set; } = null!;

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("surname")]
    [StringLength(50)]
    public string Surname { get; set; } = null!;

    [Column("middle_name")]
    [StringLength(50)]
    public string? MiddleName { get; set; }

    [Column("gender")]
    [StringLength(10)]
    public string? Gender { get; set; }

    [Column("image_url")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [InverseProperty("person")]
    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();

    [InverseProperty("dispatcher")]
    public virtual ICollection<Call> Calldispatchers { get; set; } = new List<Call>();

    [InverseProperty("person")]
    public virtual ICollection<Call> Callpeople { get; set; } = new List<Call>();

    [ForeignKey("card_id")]
    [InverseProperty("People")]
    public virtual MedicalCard? Card { get; set; }
}
