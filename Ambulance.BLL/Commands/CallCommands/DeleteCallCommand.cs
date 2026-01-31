using AutoMapper;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities.StandartEnums;

namespace Ambulance.BLL.Commands.CallCommands;

public class DeleteCallCommand : AbstrCommandWithDA<bool>
{
    private readonly int callId;

    public override string Name => "Видалення виклику";

    public DeleteCallCommand(int callId, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.callId = callId;
    }

    public override bool Execute()
    {
        var call = dAPoint.CallRepository
            .FirstOrDefault(x => x.CallId == callId);

        ArgumentNullException.ThrowIfNull(call, $"Дзвінок з ID {callId} не знайдений");

        var callBrigades = dAPoint.BrigadeRepository
            .GetQueryable()
            .Where(b => b.CurrentCallId == callId);

        foreach (var brigade in callBrigades)
        {
            brigade.CurrentCallId = null;
            brigade.BrigadeState = BrigadeState.Free.ToString();

            dAPoint.BrigadeRepository.Update(brigade);
        }

        dAPoint.CallRepository.Remove(callId);
        dAPoint.Save(); // потім видаляємо виклик

        return true;
    }
}
