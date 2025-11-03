using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

public class LoadPersonsCommand : AbstrCommandWithDA<List<PersonExtModel>>
{
    private readonly int actionPersonId;

    public override string Name => "Отримання всіх Person";

    public LoadPersonsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper, int actionPersonId)
        : base(operateUnitOfWork, mapper)
    {
        ValidateIn(actionPersonId);

        this.actionPersonId = actionPersonId;
    }

    public override List<PersonExtModel> Execute()
    {
        var persons = dAPoint.PersonRepository.GetQueryable()
            .Include(p => p.UserRole)
            .ToList();

        var result = mapper.Map<List<PersonExtModel>>(persons);

        LogAction($"{Name}: отримано {result.Count} користувачів", actionPersonId);

        return result;
    }
}
