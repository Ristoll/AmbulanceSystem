using AmbulanceSystem.Core.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands;

public class PersonIdentityCommandManager : AbstractCommandManager
{
    public PersonIdentityCommandManager(IUnitOfWork unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }


}
