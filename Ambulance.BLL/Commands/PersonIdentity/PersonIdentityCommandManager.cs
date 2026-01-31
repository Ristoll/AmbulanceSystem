using AutoMapper;
using AmbulanceSystem.DAL;
using AmbulanceSystem.Core;
using Ambulance.DTO.PersonModels;
using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.BLL.Commands.PersonIdentity.PICommands;

namespace Ambulance.BLL.Commands;

public class PersonIdentityCommandManager : AbstractCommandManager
{
    private IImageService imageService;

    public PersonIdentityCommandManager(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService) 
        : base(unitOfWork, mapper) 
    {
        ArgumentNullException.ThrowIfNull(imageService);

        this.imageService = imageService;
    }

    public int CreatePerson(PersonCreateRequest createUserModel)
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
        var command = new GetPersonProfileCommand(unitOfWork, mapper, personId, imageService);
        return ExecuteCommand(command, "Не вдалося отримати профіль користувача");
    }

    public PatientDto LoadPatientData(int personId)
    {
        var command = new LoadPatientDataCommand(personId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося отримати дані пацієнта");
    }
    
    public bool UpdatePerson(PersonUpdateDTO updateModel)
    {
        var command = new UpdatePersonCommand(unitOfWork, mapper, updateModel, imageService);
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
