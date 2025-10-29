using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class DeletePersonCommand : AbstrCommandWithDA<bool>
{
    private readonly int personId;

    public override string Name => "Видалення Person";

    public DeletePersonCommand(IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext, int personId)
        : base(unitOfWork, mapper, userContext)
    {
        if(personId <= 0)
        {
            throw new ArgumentException("Некоректний ID користувача для видалення");
        }

        this.personId = personId;
    }

    public override bool Execute()
    {
        var person = dAPoint.PersonRepository.GetById(personId);

        if (person == null)
        {
            throw new ArgumentException($"персона з ID {personId} не знайдена в системі");
        }

        dAPoint.CallRepository.Remove(personId);
        dAPoint.Save();

        LogAction($"{Name}: персона з ID {personId} видалена");
        return true;
    }
}
