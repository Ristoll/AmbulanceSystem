using Ambulance.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System.Text.RegularExpressions;

namespace Ambulance.BLL.Commands.PersonIdentity;

public class CreatePersonCommand : AbstrCommandWithDA <bool>
{
    override public string Name => "Створення Person";
    private readonly PersonCreateModel createUserModel;
    private readonly int personId;

    public CreatePersonCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, PersonCreateModel createUserModel, int personId)
        : base(operateUnitOfWork, mapper)
    {
        ValidateIn(createUserModel, personId);

        this.createUserModel = createUserModel;
        this.personId = personId;
    }

    public override bool Execute()
    {
        var newPerson = mapper.Map<Person>(createUserModel);

        var roleEntity = dAPoint.UserRoleRepository
            .FirstOrDefault(r => r.UserRoleId == createUserModel.RoleId);

        if (roleEntity == null)
            throw new ArgumentException($"Роль з Id '{createUserModel.RoleId}' не знайдена");

        newPerson.UserRole = roleEntity;

        dAPoint.PersonRepository.Add(newPerson);
        dAPoint.Save();

        LogAction($"{Name}: Був створений новий Person: {newPerson.Login}", personId);
        return true;
    }

    protected void ValidateIn(PersonCreateModel createUserModel, int actPersonId)
    {
        var existingActionPerson = dAPoint.PersonRepository
            .FirstOrDefault(p => p.PersonId == actPersonId);

        if (existingActionPerson == null)
            throw new ArgumentException($"Некоректний виконавець дії '{actPersonId}'");

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
