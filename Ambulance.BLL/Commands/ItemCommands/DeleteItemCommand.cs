using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class DeleteItemCommand : AbstrCommandWithDA<bool>
    {
        private readonly int itemId;
        private readonly int actorId;

        public DeleteItemCommand(int itemId, int actorId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.itemId = itemId;
            this.actorId = actorId;
        }

        public override string Name => "Видалення медикаменту";

        public override bool Execute()
        {
            if (dAPoint.ItemRepository.FirstOrDefault(i => i.ItemId == itemId) != null)
            {
                dAPoint.ItemRepository.Remove(itemId);
                dAPoint.Save();
                LogAction($"{Name} з ID {itemId}", actorId);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
