using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands;

public abstract class AbstrCommandWithDA<TResult> : IBaseCommand<TResult>
{
    protected readonly IUnitOfWork dAPoint;
    protected readonly IMapper mapper;
    protected IUserContext userContext;

    public abstract string Name { get; }

    protected AbstrCommandWithDA(IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext)
    {
        this.dAPoint = operateUnitOfWork;
        this.mapper = mapper;
        this.userContext = userContext;
    }

    protected void LogAction(string actionDescription)
    {
        var personId = userContext.CurrentUserId;

        if (personId == null)
            throw new InvalidOperationException("Не вдалося визначити користувача для логування дії");

        var log = new ActionLog(actionDescription, personId.Value);
        dAPoint.ActionLogRepository.Add(log);
        dAPoint.Save();
    }

    public abstract TResult Execute();
}
