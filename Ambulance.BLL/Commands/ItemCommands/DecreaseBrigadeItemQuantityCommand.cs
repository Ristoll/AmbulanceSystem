using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class DecreaseBrigadeItemQuantityCommand : AbstrCommandWithDA<bool>
    {
        private readonly int brigadeItemId;
        private readonly int quantity;

        public DecreaseBrigadeItemQuantityCommand(int brigadeItemId, int quantity, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.brigadeItemId = brigadeItemId;
            this.quantity = quantity;
        }

        public override string Name => "Зменшення кількості медикамента";

        public override bool Execute()
        {
            var brigadeItem = dAPoint.BrigadeItemRepository.FirstOrDefault(bi => bi.BrigadeItemId == brigadeItemId);
            if (brigadeItem != null && brigadeItem.Quantity > 0)
            {
                brigadeItem.Quantity -= quantity;
                dAPoint.BrigadeItemRepository.Update(brigadeItem);
                dAPoint.Save();
          
                return true;
            }
            else
            {
                throw new ArgumentException($"{Name}: Неможливо зменшити кількість медикамента з ID {brigadeItemId}. Медикамент не знайдений або кількість вже нульова");
            }
        }
    }
}
