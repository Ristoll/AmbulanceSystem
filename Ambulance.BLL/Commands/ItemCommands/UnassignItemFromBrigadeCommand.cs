using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class UnassignItemToBrigadeCommand : AbstrCommandWithDA<bool>
    {
        private readonly int brigadeItemId;
        private readonly int actorId;

        public UnassignItemToBrigadeCommand(int brigadeItemId, int actorId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.brigadeItemId = brigadeItemId;
            this.actorId = actorId;
        }

        public override string Name => "Винесення медикамента з бригади";

        public override bool Execute()
        {
            var brigadeItem = dAPoint.BrigadeItemRepository
                .FirstOrDefault(bi => bi.BrigadeItemId == brigadeItemId);

            dAPoint.BrigadeItemRepository.Remove(brigadeItemId);
            dAPoint.Save();
            LogAction($"{Name} з ID {brigadeItem!.BrigadeId}", actorId);
            return true;
        }
    }
}
