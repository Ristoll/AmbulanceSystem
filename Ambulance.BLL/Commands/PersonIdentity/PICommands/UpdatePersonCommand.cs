using Ambulance.Core.Entities;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DAL;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class UpdatePersonCommand : AbstrCommandWithDA<bool>
{
    private readonly PersonUpdateDTO model;
    private readonly IImageService imageService;

    public override string Name => "Оновлення Person";

    public UpdatePersonCommand(
        IUnitOfWork operateUnitOfWork,
        IMapper mapper,
        PersonUpdateDTO model,
        IImageService imageService)
        : base(operateUnitOfWork, mapper)
    {
        this.model = model ?? throw new ArgumentNullException(nameof(model));
        this.imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
    }

    public override bool Execute()
    {
        if (model.PersonId == null)
            throw new InvalidOperationException("Ідентифікатор користувача не вказано");

        var person = dAPoint.PersonRepository.GetById(model.PersonId.Value)
            ?? throw new InvalidOperationException("Користувача не знайдено");

        UpdateGender(person);
        UpdateRole(person);
        UpdateImage(person);
        UpdateScalarFields(person);

        dAPoint.Save();
        return true;
    }

    private void UpdateGender(Person person)
    {
        if (!string.IsNullOrWhiteSpace(model.Gender))
        {
            var gender = EnumConverters.ParseUserGender(model.Gender);
            if (gender != person.Gender)
            {
                person.Gender = gender;
            }
        }
    }

    private void UpdateRole(Person person)
    {
        if (!string.IsNullOrWhiteSpace(model.Role))
        {
            var role = EnumConverters.ParseUserRole(model.Role);
            if (role != person.UserRole)
            {
                person.UserRole = role;
            }
        }
    }

    private void UpdateImage(Person person)
    {
        if (model.Image == null || model.Image.Bytes == null || model.Image.Bytes.Length == 0)
            return;

        var extension = NormalizeExtension(model.Image.ContentType);
        var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + extension);

        File.WriteAllBytes(tempFile, model.Image.Bytes);

        try
        {
            var relativePath = imageService.SaveImage(tempFile);
            person.ImageUrl = relativePath;
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    private void UpdateScalarFields(Person person)
    {
        foreach (var prop in typeof(PersonUpdateDTO).GetProperties())
        {
            if (prop.Name is
                nameof(PersonUpdateDTO.PersonId) or
                nameof(PersonUpdateDTO.Role) or
                nameof(PersonUpdateDTO.Gender) or
                nameof(PersonUpdateDTO.Image))
                continue;

            var newValue = prop.GetValue(model);
            if (newValue == null) continue;

            var targetProp = typeof(Person).GetProperty(prop.Name);
            if (targetProp == null || !targetProp.CanWrite) continue;

            var currentValue = targetProp.GetValue(person);

            if (!Equals(currentValue, newValue))
            {
                targetProp.SetValue(person, newValue);
            }
        }
    }

    private static string NormalizeExtension(string? contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            return ".jpg";

        // якщо прийшло "image/jpeg"
        if (contentType.Contains('/'))
        {
            var ext = contentType.Split('/').Last();
            return "." + ext.Replace("jpeg", "jpg");
        }

        return contentType.StartsWith('.')
            ? contentType.ToLowerInvariant()
            : "." + contentType.ToLowerInvariant();
    }
}
