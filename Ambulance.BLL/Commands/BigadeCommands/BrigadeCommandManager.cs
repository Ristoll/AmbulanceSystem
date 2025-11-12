using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.BigadeCommands
{
    public class BrigadeCommandManager : AbstractCommandManager
    {
        public BrigadeCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
       : base(unitOfWork, mapper) { }
        public bool ChangeBrigadeStateCommand(int brigadeStateId, BrigadeDto brigade, int actionOwnerID)
        {
            var command = new ChangeBrigadeStateCommand(brigadeStateId, brigade, unitOfWork, mapper, actionOwnerID);
            return ExecuteCommand(command, "Не вдалося змінити стан бригади");
        }
        public BrigadeDto UpdateBrigade(BrigadeDto brigadeDto, int actionOwnerID)
        {
            var command = new UpdateBrigade(brigadeDto, unitOfWork, mapper, actionOwnerID);
            return ExecuteCommand(command, "Не вдалося оновити дані бригади");
        }
    }
}
