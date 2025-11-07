using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class GetPersonProfileCommand : AbstrCommandWithDA<PersonProfileDTO>
{
    private readonly int personId;

    public override string Name => "Отримання профілю користувача";

    public GetPersonProfileCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, int personId)
        : base(operateUnitOfWork, mapper)
    {
        ValidateIn(personId);

        this.personId = personId;
    }

    public override PersonProfileDTO Execute()
    {
        var person = dAPoint.PersonRepository.GetById(personId);

        if (person == null)
            throw new InvalidOperationException("Користувача не знайдено");

        var result = mapper.Map<PersonProfileDTO>(person);
        return result;
    }
}
