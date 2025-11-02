using Ambulance.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class DeletePersonCommand : AbstrCommandWithDA<bool>
{
    private readonly int deletePersonId;
    private readonly int actionPersonId;

    public override string Name => "Видалення Person";

    public DeletePersonCommand(IUnitOfWork unitOfWork, IMapper mapper, int deletePersonId, int actionPersonId)
        : base(unitOfWork, mapper)
    {
        ValidateIn(actionPersonId);

        this.deletePersonId = deletePersonId;
        this.actionPersonId = actionPersonId;
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

        LogAction($"{Name}: персона з ID {deletePersonId} видалена", actionPersonId);
        return true;
    }
}
