using Ambulance.BLL.Models.PersonModels;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class GetPersonProfileCommand : AbstrCommandWithDA<PersonProfileModel>
{
    private readonly int personId;

    public override string Name => "Отримання профілю користувача";

    public GetPersonProfileCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, int personId)
        : base(operateUnitOfWork, mapper)
    {
        ValidateIn(personId);

        this.personId = personId;
    }

    public override PersonProfileModel Execute()
    {
        var person = dAPoint.PersonRepository.GetById(personId);

        if (person == null)
            throw new InvalidOperationException("Користувача не знайдено");

        var result = mapper.Map<PersonProfileModel>(person);
        return result;
    }
}
