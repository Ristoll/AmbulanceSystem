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
    public class ItemCommandManager : AbstractCommandManager
    {
        public ItemCommandManager(IUnitOfWork unitOfWork, IMapper mapper)
       : base(unitOfWork, mapper) { }

        public bool CreateItemCommand(ItemDto itemDto,int actionOwnerID)
        {
            var command = new CreateItemCommand(itemDto, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося створити предмет");
        }
        public bool DeleteItemCommand(int itemId, int actionOwnerID)
        {
            var command = new DeleteItemCommand(itemId, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося видалити предмет");
        }
        public bool SearchItemCommand(int itemId, int actionOwnerID)
        {
            var command = new SearchItemCommand(itemId, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося знайти предмет");
        }
        public List<ItemDto> LoadItemsCommand()
        {
            var command = new LoadItemsCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити предмети");
        }
        public bool AssignItemToBrigadeCommand(BrigadeItemDto brigadeItemDto, int actionOwnerID)
        {
            var command = new AssignItemToBrigadeCommand(brigadeItemDto, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося призначити предмет бригаді");
        }
        public bool UnassignItemFromBrigadeCommand(int brigadeItemId, int actionOwnerID)
        {
            var command = new UnassignItemFromBrigadeCommand(brigadeItemId, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося відмінити призначення предмета бригаді");
        }

        public List<BrigadeItemDto> LoadBrigadeItemsCommand()
        {
            var command = new LoadBrigadeItemsCommand(unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося завантажити предмети бригади");
        }
        public bool IncreaseBrigadeItemQuantityCommand(int itemId, int amount, int actionOwnerID)
        {
            var command = new IncreaseBrigadeItemQuantityCommand(itemId, amount, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося збільшити кількість предмета");
        }
        public bool DecreaseBrigadeItemQuantityCommand(int itemId, int amount, int actionOwnerID)
        {
            var command = new DecreaseBrigadeItemQuantityCommand(itemId, amount, actionOwnerID, unitOfWork, mapper);
            return ExecuteCommand(command, "Не вдалося зменшити кількість предмета");
        }
    }
}
