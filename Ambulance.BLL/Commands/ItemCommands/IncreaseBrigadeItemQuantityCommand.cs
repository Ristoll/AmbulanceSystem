using AutoMapper;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class IncreaseBrigadeItemQuantityCommand : AbstrCommandWithDA<bool>
    {
        private readonly int brigadeItemId;
        private readonly int quantity;

        public IncreaseBrigadeItemQuantityCommand(int brigadeItemId, int quantity, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.brigadeItemId = brigadeItemId;
            this.quantity = quantity;
        }

        public override string Name => "Збільшення кількості медикамента";

        public override bool Execute()
        {
            var brigadeItem = dAPoint.BrigadeItemRepository
                .FirstOrDefault(bi => bi.BrigadeItemId == brigadeItemId);

            if (brigadeItem != null && brigadeItem.Quantity > 0)
            {
                brigadeItem.Quantity += quantity;
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

