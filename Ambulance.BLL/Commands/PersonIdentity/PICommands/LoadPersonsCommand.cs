using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class LoadPersonsCommand : AbstrCommandWithDA<List<PersonExtDTO>>
{
    private readonly int actionPersonId;

    public override string Name => "Отримання всіх Person";

    public LoadPersonsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
    }

    public override List<PersonExtDTO> Execute()
    {
        var persons = dAPoint.PersonRepository.GetAll();

        var result = mapper.Map<List<PersonExtDTO>>(persons);

        return result;
    }
}
