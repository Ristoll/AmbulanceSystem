using Ambulance.Core;
using Ambulance.Core.Entities.StandartEnums;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.BLL.Commands.CallCommands
{
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
                .GetQueryable()
                .Include(x => x.Brigades)
                .FirstOrDefault(x => x.CallId == callId);

            ArgumentNullException.ThrowIfNull(call);

            foreach (var brigade in call.Brigades.ToList())
            {
                brigade.CurrentCallId = null;
                brigade.BrigadeState = BrigadeState.Free.ToString();

                dAPoint.BrigadeRepository.Update(brigade);
            }

            dAPoint.Save(); // спочатку оновлюємо бригади

            dAPoint.CallRepository.Remove(callId);
            dAPoint.Save(); // потім видаляємо виклик

            return true;
        }
    }
}
