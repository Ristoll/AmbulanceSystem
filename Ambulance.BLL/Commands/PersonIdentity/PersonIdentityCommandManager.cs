using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.BLL.Models;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AutoMapper;

namespace Ambulance.BLL.Commands;

public class PersonIdentityCommandManager : AbstractCommandManager
{
    public PersonIdentityCommandManager(IUnitOfWork unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }

    public bool CreatePerson(PersonCreateModel createUserModel, int actionOwberID)
    {
        var command = new CreatePersonCommand(unitOfWork, mapper, createUserModel, actionOwberID);
        return ExecuteCommand(command, "Не вдалося створити користувача");
    }

    public PersonExtModel AuthPerson(string login, string password)
    {
        var command = new AuthCommand(unitOfWork, mapper, login, password);
        return ExecuteCommand(command, "Не вдалося автентифікувати користувача");
    }
}
