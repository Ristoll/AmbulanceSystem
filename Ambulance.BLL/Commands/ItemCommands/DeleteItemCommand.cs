using AutoMapper;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.ItemCommands;

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
        dAPoint.ItemRepository.Remove(itemId);
        dAPoint.Save();

        return true;
    }
}
