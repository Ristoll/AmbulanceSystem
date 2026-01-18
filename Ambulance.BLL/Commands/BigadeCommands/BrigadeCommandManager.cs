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
    public bool ChangeBrigadeState(string stateName, int brigadeId)
    {
        var command = new ChangeBrigadeStateCommand(stateName, brigadeId, unitOfWork, mapper);
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
    public List<BrigadeDto> LoadBrigadesByState(string brigadesStName)
    {
        var command = new LoadBrigadesByStateCommand(unitOfWork, mapper, brigadesStName);
        return ExecuteCommand(command, "Не вдалося підвантажити бригади за станом");
    }
    public BrigadeDto LoadBrigade(int brigadeId)
    {
        var command = new LoadBrigadeCommand(brigadeId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося підвантажити бригаду за ID");
    }
    public BrigadeTypeDto LoadBrigadeType(int brigadeId)
    {
        var command = new LoadBrigadeTypeCommand(brigadeId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося підвантажити тип бригади за ID");
    }
    public List<BrigadeMemberDto> LoadAllBrigadeMembers(int brigadeId)
    {
        var command = new LoadAllBrigadeMembersCommand(brigadeId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося підвантажити всіх членів бригад");
    }
    public BrigadeMemberDto LoadBrigadeMember(int brigadeMemberId)
    {
        var command = new LoadBrigadeMemberCommand(brigadeMemberId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося підвантажити члена бригади за ID");
    }
    public string LoadBrigadeMemberRoleName(int brigadeMemberId)
    {
        var command = new LoadBrigadeMemberRoleCommand(brigadeMemberId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося підвантажити назву ролі члена бригади за ID");
    }
    public string LoadBrigadeMemberSpecializationTypeName(int brigadeMemberId)
    {
        var command = new LoadBrigadeMemberSpecialisationTypeCommand(brigadeMemberId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося підвантажити назву спеціалізації члена бригади за ID");
    }
}
