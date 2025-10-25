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

    protected void LogAction(string actionDescription, int actionOwnerID)
    {
        Person? actionOwner = dAPoint.PersonRepository.FirstOrDefault(p => p.PersonId == actionOwnerID);

        ArgumentNullException.ThrowIfNull(actionOwner, "Виконавець дії відсутній");

        var logEntry = new ActionLog(actionDescription, actionOwner);

        dAPoint.LogRepository.Add(logEntry);
        dAPoint.Save();
    }

    public abstract TResult Execute();
}
