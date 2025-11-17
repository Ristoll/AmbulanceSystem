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

        public DeleteCallCommand(int callId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.callId = callId;
        }

        public override bool Execute()
        {
            var call = dAPoint.CallRepository.GetById(callId);

            ArgumentNullException.ThrowIfNull(call);

            dAPoint.CallRepository.Remove(callId);
            dAPoint.Save();

            return true;
        }
    }
}
