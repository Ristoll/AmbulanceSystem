using Ambulance.Core.Entities;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class UpdatePersonCommand : AbstrCommandWithDA<bool>
{
    private readonly PersonUpdateDTO upPersonModel;

    public override string Name => "Оновлення Person";

    public UpdatePersonCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, PersonUpdateDTO upPersonModel)
        : base(operateUnitOfWork, mapper)
    {
        this.upPersonModel = upPersonModel;
    }

    public override bool Execute()
    {
        var person = dAPoint.PersonRepository.GetById(upPersonModel.PersonId);

        if (person == null)
            throw new InvalidOperationException("Користувача не знайдено");

        // обробка гендеру окремо
        if (!string.IsNullOrEmpty(upPersonModel.Gender))
        {
            var newGender = EnumConverters.ParseUserGender(upPersonModel.Gender);

            if (newGender != person.Gender)
            {
                person.Gender = newGender;
            }
        }

        // обробка ролі окремо
        if (upPersonModel.Role != null)
        {
            var newRole = EnumConverters.ParseUserRole(upPersonModel.Role);

            if (newRole != person.UserRole)
            {
                person.UserRole = newRole;
            }
        }

        foreach (var prop in typeof(PersonUpdateDTO).GetProperties())
        {
            var newValue = prop.GetValue(upPersonModel);

            if (newValue != null)
            {
                if (prop.Name is nameof(PersonUpdateDTO.PersonId)
                    or nameof(PersonUpdateDTO.Gender)
                    or nameof(PersonUpdateDTO.Role))
                    continue;  // за винятком ID, ці властивості обробляємо вручну

                var targetProp = typeof(Person).GetProperty(prop.Name);

                if (targetProp != null && targetProp.CanWrite)  // додаткова перевірка на запис
                {
                    var currentValue = targetProp.GetValue(person);

                    if (!newValue.Equals(currentValue))
                    {
                        targetProp.SetValue(person, newValue);
                    }
                }
            }
        }

        dAPoint.Save(); // EF оновить лише змінені поля

        return true;
    }
}
