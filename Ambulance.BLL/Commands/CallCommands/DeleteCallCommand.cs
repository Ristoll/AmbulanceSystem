using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.CallCommands
{
    public class DeleteCallCommand : AbstrCommandWithDA<bool>
    {
        private readonly int callId;
        private readonly int personId;

        public override string Name => "Видалення виклику";

        public DeleteCallCommand(int callId, IUnitOfWork unitOfWork, IMapper mapper, int personId)
            : base(unitOfWork, mapper)
        {
            this.callId = callId;
            this.personId = personId;
        }

        public override bool Execute()
        {
            var call = dAPoint.CallRepository.GetById(callId);
            if (call == null)
            {
                LogAction($"{Name}: виклик з ID {callId} не знайдено", personId);
                return false;
            }

            dAPoint.CallRepository.Remove(callId);
            dAPoint.Save();

            LogAction($"{Name}: виклик № {callId} видалено", personId);
            return true;
        }
    }
}
