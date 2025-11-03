using Ambulance.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class UpdatePersonCommand : AbstrCommandWithDA<bool>
{
    private readonly int? actionPersonId;
    private readonly PersonUpdateModel upPersonModel;

    public override string Name => "Оновлення Person";

    public UpdatePersonCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, PersonUpdateModel upPersonModel, int? actionPersonId)
        : base(operateUnitOfWork, mapper)
    {
        if (actionPersonId.HasValue)
        {
            ValidateIn(actionPersonId.Value);
            this.actionPersonId = actionPersonId;
        }

        this.upPersonModel = upPersonModel;
    }

    public override bool Execute()
    {
        var person = dAPoint.PersonRepository.GetById(upPersonModel.PersonId);

        if (person == null)
            throw new InvalidOperationException("Користувача не знайдено");

        List<string> changesLog = new List<string>();

        // обробка гендеру окремо
        if (!string.IsNullOrEmpty(upPersonModel.Gender))
        {
            var newGender = EnumConverters.ParseUserGender(upPersonModel.Gender);

            if (newGender != person.Gender)
            {
                changesLog.Add($"Gender змінено з '{person.Gender}' на '{newGender}'");
                person.Gender = newGender;
            }
        }

        // обробка ролі окремо
        if (upPersonModel.RoleId.HasValue)
        {
            var roleEntity = dAPoint.UserRoleRepository.GetById(upPersonModel.RoleId.Value);

            if (roleEntity == null)
                throw new ArgumentException($"Роль з Id '{upPersonModel.RoleId}' не знайдена");

            person.UserRole = roleEntity;
        }

        foreach (var prop in typeof(PersonUpdateModel).GetProperties())
        {
            var newValue = prop.GetValue(upPersonModel);

            if (newValue != null)
            {
                if (prop.Name is nameof(PersonUpdateModel.PersonId)
                    or nameof(PersonUpdateModel.Gender)
                    or nameof(PersonUpdateModel.RoleId))
                    continue;  // за винятком ID, ці властивості обробляємо вручну

                var targetProp = typeof(Person).GetProperty(prop.Name);

                if (targetProp != null && targetProp.CanWrite)  // додаткова перевірка на запис
                {
                    var currentValue = targetProp.GetValue(person);

                    if (!newValue.Equals(currentValue))
                    {
                        targetProp.SetValue(person, newValue);
                        changesLog.Add($"{prop.Name} змінено з '{currentValue}' на '{newValue}'");
                    }
                }
            }
        }

        dAPoint.Save(); // EF оновить лише змінені поля

        if (actionPersonId.HasValue)
        {
            LogAction($"{Name}: Адміністратором змінені дані користувача ({string.Join(", ", changesLog)})", actionPersonId.Value);
        }
        else
        {
            LogAction($"{Name}: Користувач змінив свої дані ({string.Join(", ", changesLog)})", person.PersonId);
        }


        return true;
    }
}
