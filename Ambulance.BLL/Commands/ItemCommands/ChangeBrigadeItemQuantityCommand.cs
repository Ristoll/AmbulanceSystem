using AutoMapper;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.ItemCommands;

// запитати Крістіну, команда на заміну двох Increase - Decrease
internal class ChangeBrigadeItemQuantityCommand : AbstrCommandWithDA<bool>
{
    private readonly int brigadeItemId;
    private readonly int quantity;

    public ChangeBrigadeItemQuantityCommand(int brigadeItemId, int quantity, IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
        this.brigadeItemId = brigadeItemId;
        this.quantity = quantity;
    }

    public override string Name => "Зміна кількості медикамента";

    public override bool Execute()
    {
        if (quantity == 0) // просто скіпаємо залишок для оптимізації
            return true;

        var brigadeItem = dAPoint.BrigadeItemRepository
            .FirstOrDefault(bi => bi.BrigadeItemId == brigadeItemId);

        if (brigadeItem == null)
            throw new ArgumentException($"Медикамент з ID {brigadeItemId} не знайдений");

        brigadeItem.Quantity += quantity;

        if (brigadeItem.Quantity < 0) // запитати Крістіну щодо constaint на рівні БД
            brigadeItem.Quantity = 0;

        dAPoint.BrigadeItemRepository.Update(brigadeItem);
        dAPoint.Save();

        return true;
    }
}
