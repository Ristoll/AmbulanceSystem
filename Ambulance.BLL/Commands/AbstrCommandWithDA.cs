using AmbulanceSystem.Core.Data;
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

    protected void LogAction(string actionDescription)
    {
        var logEntry = new ActionLog(actionDescription);

        dAPoint.LogRepository.Add(logEntry);
        dAPoint.Save();
    }

    public abstract TResult Execute();
}
