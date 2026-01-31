using System;
using System.Collections.Generic;

namespace Ambulance.Core.Entities;

public partial class Person
{
    public int PersonId { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string UserRole { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? Gender { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<BrigadeMember> BrigadeMembers { get; set; } = new List<BrigadeMember>();

    public virtual ICollection<Call> CallDispatchers { get; set; } = new List<Call>();

    public virtual ICollection<Call> CallPeople { get; set; } = new List<Call>();

    public virtual MedicalCard? MedicalCard { get; set; }
}
