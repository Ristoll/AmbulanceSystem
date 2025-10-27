using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System.Text.RegularExpressions;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class CreatePersonCommand : AbstrCommandWithDA <bool>
{
    override public string Name => "Створення Person";
    private readonly PersonCreateModel createUserModel;
    private readonly int actionOwberID;

    public CreatePersonCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, PersonCreateModel createUserModel, IUserContext userContext)
        : base(operateUnitOfWork, mapper, userContext)
    {
        ValidateModel(createUserModel);

        this.createUserModel = createUserModel;
    }

    public override bool Execute()
    {
        var alreadyExistingPerson = dAPoint.PersonRepository.FirstOrDefault(p => p.Login == createUserModel.Login);

        if (alreadyExistingPerson != null)
        {
            return false;
        }

        // усі провірки пройдено = мапимо модель в ентіті
        var newPerson = mapper.Map<Person>(createUserModel);

        dAPoint.PersonRepository.Add(newPerson);
        dAPoint.Save();

        LogAction($"Був створений новий Person: {newPerson.Login}");
        return true;
    }

    private void ValidateModel(PersonCreateModel createUserModel)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(createUserModel.Name))
            errors.Add("Ім'я користувача обов'язкове і не може бути порожнім");

        if (string.IsNullOrWhiteSpace(createUserModel.Surname))
            errors.Add("Прізвище користувача обов'язкове і не може бути порожнім");

        if (string.IsNullOrWhiteSpace(createUserModel.Login))
        {
            errors.Add("Логін користувача обов'язковий і не може бути порожнім");
        }
        else
        {
            // перевірка на унікальність логіну
            var existingPerson = dAPoint.PersonRepository
                .FirstOrDefault(p => p.Login == createUserModel.Login);

            if (existingPerson != null)
                errors.Add($"Користувач з логіном '{createUserModel.Login}' уже існує");
        }

        if (!string.IsNullOrWhiteSpace(createUserModel.Email) &&
            !Regex.IsMatch(createUserModel.Email, @"^\S+@\S+\.\S+$"))
        {
            errors.Add("Невірний формат електронної пошти");
        }

        if (!string.IsNullOrWhiteSpace(createUserModel.PhoneNumber) &&
            !Regex.IsMatch(createUserModel.PhoneNumber, @"^\+?\d{9,15}$"))
        {
            errors.Add("Невірний формат номеру телефону");
        }

        if (string.IsNullOrWhiteSpace(createUserModel.Password) || createUserModel.Password.Length < 8)
            errors.Add("Пароль повинен містити щонайменше 8 символів");

        if (createUserModel.DateOfBirth.HasValue)
        {
            var dob = createUserModel.DateOfBirth.Value;
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (dob > today)
                errors.Add("Дата народження не може бути в майбутньому");

            var age = today.Year - dob.Year;

            if (age > 120)
                errors.Add("Вік користувача не може перевищувати 120 років");
        }

        if (errors.Any())
            throw new ArgumentException($"Помилки валідації моделі: {string.Join("; ", errors)}");
    }
}
