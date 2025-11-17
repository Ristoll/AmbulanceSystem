using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class DeletePersonCommand : AbstrCommandWithDA<bool>
{
    private readonly int deletePersonId;
    private readonly int actionPersonId;

    public override string Name => "Видалення Person";

    public DeletePersonCommand(IUnitOfWork unitOfWork, IMapper mapper, int deletePersonId)
        : base(unitOfWork, mapper)
    {
        this.deletePersonId = deletePersonId;
    }

    public override bool Execute()
    {
        var person = dAPoint.PersonRepository.GetById(deletePersonId);

        if (person == null)
        {
            throw new ArgumentException($"Персона з ID {deletePersonId} не знайдена в системі");
        }

        dAPoint.CallRepository.Remove(deletePersonId);
        dAPoint.Save();

        return true;
    }
}
