using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.BLL.Commands.PersonIdentity.PICommands;
using Ambulance.Core.Entities;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands;

public class PersonIdentityCommandManager : AbstractCommandManager
{
    public PersonIdentityCommandManager(IUnitOfWork unitOfWork, IMapper mapper) 
        : base(unitOfWork, mapper) { }

    public bool CreatePerson(PersonCreateRequest createUserModel)
    {
        var command = new CreatePersonCommand(unitOfWork, mapper, createUserModel);
        return ExecuteCommand(command, "Не вдалося створити користувача");
    }

    public AuthResponse AuthPerson(LoginRequest request)
    {
        var command = new AuthCommand(unitOfWork, mapper, request);
        return ExecuteCommand(command, "Не вдалося автентифікувати користувача");
    }

    public bool ChangePassword(ChangePasswordRequest changePasswordModel, int userId)
    {
        var command = new ChangePasswordCommand(unitOfWork, mapper, changePasswordModel, userId);
        return ExecuteCommand(command, "Не вдалося змінити пароль");
    }

    public bool AdminResetPassword(string newPassword, int targetPersonId)
    {
        var command = new AdminResetPasswordCommand(unitOfWork, mapper, newPassword, targetPersonId);
        return ExecuteCommand(command, "Не вдалося скинути пароль користувача");
    }

    public PersonProfileDTO GetPersonProfile(int personId)
    {
        var command = new GetPersonProfileCommand(unitOfWork, mapper, personId);
        return ExecuteCommand(command, "Не вдалося отримати профіль користувача");
    }

    public bool UpdatePerson(PersonUpdateDTO updateModel)
    {
        var command = new UpdatePersonCommand(unitOfWork, mapper, updateModel);
        return ExecuteCommand(command, "Не вдалося оновити дані користувача");
    }

    public bool DeletePerson(int deletePersonId)
    {
        var command = new DeletePersonCommand(unitOfWork, mapper, deletePersonId);
        return ExecuteCommand(command, "Не вдалося видалити користувача");
    }

    public List<PersonExtDTO> LoadPersons()
    {
        var command = new LoadPersonsCommand(unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося завантажити список користувачів");
    }

    public List<string> LoadPersonRoles()
    {
        var command = new LoadPersonRolesCommand(unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося отримати список ролей користувачів");
    }
}
