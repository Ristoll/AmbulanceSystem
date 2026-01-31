using AutoMapper;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.ItemCommands;

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

        ArgumentNullException.ThrowIfNull(item, $"{Name}: Медикамент не знайдений з ID {itemId}");

        return true;
    }
}
