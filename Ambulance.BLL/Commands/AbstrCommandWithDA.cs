using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands;

public abstract class AbstrCommandWithDA<TResult> : IBaseCommand<TResult>
{
    protected readonly IUnitOfWork dAPoint;
    protected readonly IMapper mapper;

    public abstract string Name { get; }

    protected AbstrCommandWithDA(IUnitOfWork operateUnitOfWork, IMapper mapper)
    {
        this.dAPoint = operateUnitOfWork;
        this.mapper = mapper;
    }

    protected void LogAction(string actionDescription, int personId)
    {
        var log = new ActionLog(actionDescription, personId);
        dAPoint.ActionLogRepository.Add(log);
        dAPoint.Save();
    }

    public abstract TResult Execute();

    protected void ValidateIn(int actionPersonId)
    {
        var existingActionPerson = dAPoint.PersonRepository
            .FirstOrDefault(p => p.PersonId == actionPersonId);

        if (existingActionPerson == null)
            throw new ArgumentException($"Некоректний виконавець дії '{actionPersonId}'");
    }
}
