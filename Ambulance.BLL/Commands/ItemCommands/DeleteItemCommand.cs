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

        public DeleteItemCommand(int itemId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.itemId = itemId;
        }

        public override string Name => "Видалення медикаменту";

        public override bool Execute()
        {
            if (dAPoint.ItemRepository.FirstOrDefault(i => i.ItemId == itemId) != null)
            {
                dAPoint.ItemRepository.Remove(itemId);
                dAPoint.Save();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
