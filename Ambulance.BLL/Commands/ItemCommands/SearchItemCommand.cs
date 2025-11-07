using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class SearchItemCommand : AbstrCommandWithDA<bool>
    {
        private readonly int itemId;
        private readonly int actorId;

        public SearchItemCommand(int itemId, int actorId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.itemId = itemId;
            this.actorId = actorId;
        }

        public override string Name => "Пошук медикаментy";

        public override bool Execute()
        {
            var item = dAPoint.ItemRepository.FirstOrDefault(i => i.ItemId == itemId);
            if (item != null)
            {
                LogAction($"{Name}: Медикамент знайдений з ID {itemId}", actorId);
                return true;
            }
            else
            {
                LogAction($"{Name}: Медикамент не знайдений з ID {itemId}", actorId);
                return false;
            }
        }
    }
}
