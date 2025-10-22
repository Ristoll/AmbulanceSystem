using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbulanceSystem.BLL.Models;

public class BrigadeMemberModel
{
    public int BrigadeMemberId { get; set; }
    public int PersonId { get; set; }
    public int BrigadeId { get; set; }
    public int RoleId { get; set; }
    public int MemberSpecializationTypeId { get; set; }
    public string? PersonFullName { get; set; }
    public string? BrigadeName { get; set; }
    public string? RoleName { get; set; }
    public string? SpecializationTypeName { get; set; }
}

