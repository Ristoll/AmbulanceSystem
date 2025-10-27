using AmbulanceSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.Core;

public interface IUserContext
{
    int? CurrentUserId { get; }
}
