using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands;

public class PersonIdentityCommandManager : AbstractCommandManager
{
    public PersonIdentityCommandManager(IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext) 
        : base(unitOfWork, mapper, userContext) { }

    public bool CreatePerson(PersonCreateModel createUserModel, int actionOwberID)
    {
        var command = new CreatePersonCommand(unitOfWork, mapper, createUserModel, userContext);
        return ExecuteCommand(command, "Не вдалося створити користувача");
    }

    public AuthResponseModel AuthPerson(string login, string password)
    {
        var command = new AuthCommand(unitOfWork, mapper, userContext, login, password);
        return ExecuteCommand(command, "Не вдалося автентифікувати користувача");
    }
}
