using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.Commands.CallCommands
{
    public class DeleteCallCommand : AbstrCommandWithDA<bool>
    {
        private readonly int callId;

        public override string Name => "Видалення виклику";

        public DeleteCallCommand(int callId, IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext)
            : base(unitOfWork, mapper, userContext)
        {
            this.callId = callId;
        }

        public override bool Execute()
        {
            var call = dAPoint.CallRepository.GetById(callId);
            if (call == null)
            {
                LogAction($"{Name}: виклик з ID {callId} не знайдено");
                return false;
            }

            dAPoint.CallRepository.Remove(callId);
            dAPoint.Save();

            LogAction($"{Name}: виклик № {callId} видалено");
            return true;
        }
    }
}
