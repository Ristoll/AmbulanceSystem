using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.ItemCommands;

public class ItemCommandManager : AbstractCommandManager
{
    public ItemCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
   : base(unitOfWork, mapper) { }

    public bool CreateItemCommand(ItemDto itemDto)
    {
        var command = new CreateItemCommand(itemDto, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося створити предмет");
    }
    public bool DeleteItemCommand(int itemId)
    {
        var command = new DeleteItemCommand(itemId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося видалити предмет");
    }
    public bool SearchItemCommand(int itemId)
    {
        var command = new SearchItemCommand(itemId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося знайти предмет");
    }
    public List<ItemDto> LoadItemsCommand()
    {
        var command = new LoadItemsCommand(unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося завантажити предмети");
    }
    public bool AssignItemToBrigadeCommand(BrigadeItemDto brigadeItemDto)
    {
        var command = new AssignItemToBrigadeCommand(brigadeItemDto, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося призначити предмет бригаді");
    }
    public bool UnassignItemFromBrigadeCommand(int brigadeItemId)
    {
        var command = new UnassignItemFromBrigadeCommand(brigadeItemId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося відмінити призначення предмета бригаді");
    }

    public List<BrigadeItemDto> LoadBrigadeItemsCommand(int brigadeId)
    {
        var command = new LoadBrigadeItemsCommand(brigadeId, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося завантажити предмети бригади");
    }
    public bool IncreaseBrigadeItemQuantityCommand(int itemId, int amount)
    {
        var command = new IncreaseBrigadeItemQuantityCommand(itemId, amount, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося збільшити кількість предмета");
    }
    public bool DecreaseBrigadeItemQuantityCommand(int itemId, int amount)
    {
        var command = new DecreaseBrigadeItemQuantityCommand(itemId, amount, unitOfWork, mapper);
        return ExecuteCommand(command, "Не вдалося зменшити кількість предмета");
    }
}
