using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambulance.BLL.Commands;
using Ambulance.DTO;
using Ambulance.DTO.PersonModels;
namespace Ambulance.BLL.Commands.BigadeCommands;

public class BrigadeCommandManager : AbstractCommandManager
{
    public BrigadeCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
   : base(unitOfWork, mapper) { }
    public bool ChangeBrigadeState(int brigadeStateId, BrigadeDto brigade)
    {
        var command = new ChangeBrigadeStateCommand(brigadeStateId, brigade, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося змінити стан бригади");
    }
    public BrigadeDto UpdateBrigade(BrigadeDto brigadeDto)
    {
        var command = new UpdateBrigade(brigadeDto, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося оновити дані бригади");
    }
    public List<BrigadeDto> LoadAllBrigades()
    {
        var command = new LoadAllBrigadesCommand(unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося підвантажити всі бригади");
    }
    public List<BrigadeDto> LoadBrigadesByState(int brigadeStateId)
    {
        var command = new LoadBrigadesByStateCommand(unitOfWork, mapper);
        command.brigadeStateId = brigadeStateId;
        return ExecuteCommand(command, "Не вдалося підвантажити бригади за станом");
    }
    public BrigadeDto LoadBrigade(int brigadeId)
    {
        var command = new LoadBrigadeCommand(brigadeId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося підвантажити бригаду за ID");
    }
}
