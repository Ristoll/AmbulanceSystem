using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceSystem.DTO;

public class ItemDto
{
    public int ItemId { get; set; }

    public int ItemTypeId { get; set; }

    public string? UnitType { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }
}
