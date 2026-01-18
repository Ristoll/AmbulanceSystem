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

        public SearchItemCommand(int itemId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.itemId = itemId;
        }

        public override string Name => "Пошук медикаментy";

        public override bool Execute()
        {
            var item = dAPoint.ItemRepository.FirstOrDefault(i => i.ItemId == itemId);
            if (item != null)
            {
                return true;
            }
            else
            {
                throw new ArgumentNullException($"{Name}: Медикамент не знайдений з ID {itemId}");
            }
        }
    }
}
