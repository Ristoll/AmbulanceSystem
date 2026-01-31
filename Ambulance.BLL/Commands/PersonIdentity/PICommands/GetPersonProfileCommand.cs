using AutoMapper;
using AmbulanceSystem.Core;
using AmbulanceSystem.DAL;
using Ambulance.DTO.PersonModels;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class GetPersonProfileCommand : AbstrCommandWithDA<PersonProfileDTO>
{
    private readonly int personId;
    private readonly IImageService imageService;

    public override string Name => "Отримання профілю користувача";

    public GetPersonProfileCommand(
        IUnitOfWork operateUnitOfWork,
        IMapper mapper,
        int personId,
        IImageService imageService)
        : base(operateUnitOfWork, mapper)
    {
        this.personId = personId;
        this.imageService = imageService;
    }

    public override PersonProfileDTO Execute()
    {
        var person = dAPoint.PersonRepository.GetById(personId)
            ?? throw new InvalidOperationException("Користувача не знайдено");

        var dto = mapper.Map<PersonProfileDTO>(person);

        // додаємо ImageDto вручну
        if (!string.IsNullOrWhiteSpace(person.ImageUrl))
        {
            dto.Image = LoadImageDto(person.ImageUrl);
        }

        return dto;
    }

    private ImageDto LoadImageDto(string relativePath)
    {
        var bytes = imageService.LoadImage(relativePath);
        var extension = Path.GetExtension(relativePath);

        return new ImageDto
        {
            Bytes = bytes,
            ContentType = extension
        };
    }
}
