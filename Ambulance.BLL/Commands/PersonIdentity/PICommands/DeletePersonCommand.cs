using AutoMapper;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities.StandartEnums;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class DeletePersonCommand : AbstrCommandWithDA<bool>
{
    private readonly int deletePersonId;

    public override string Name => "Видалення Person";

    public DeletePersonCommand(IUnitOfWork unitOfWork, IMapper mapper, int deletePersonId)
        : base(unitOfWork, mapper)
    {
        this.deletePersonId = deletePersonId;
    }

    public override bool Execute()
    {
        var person = dAPoint.PersonRepository.GetById(deletePersonId);

        ArgumentNullException.ThrowIfNull(person, $"Персона з ID {deletePersonId} не знайдена в системі");

        // оновлюємо всі дзвінки, де ця особа пацієнт
        var patientCalls = dAPoint.CallRepository
            .GetQueryable()
            .Where(c => c.PersonId == deletePersonId);

        foreach (var call in patientCalls)
        {
            call.PersonId = null;
        }

        // оновлюємо всі дзвінки, де ця особа диспетчер
        if(person.UserRole == UserRole.Dispatcher.ToString())
        {
            var dispatcherCalls = dAPoint.CallRepository
                 .GetQueryable()
                 .Where(c => c.DispatcherId == deletePersonId);

            foreach (var call in dispatcherCalls)
            {
                call.DispatcherId = null;
            }
        }

        dAPoint.Save();

        dAPoint.PersonRepository.Remove(deletePersonId);
        dAPoint.Save();

        return true;
    }
}
