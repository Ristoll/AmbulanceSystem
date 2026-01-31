using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities;

namespace Ambulance.BLL.Commands.ItemCommands;

public class CreateItemCommand : AbstrCommandWithDA<bool>
{
    private readonly ItemDto itemDto;

    public CreateItemCommand(ItemDto itemDto, IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
        if (itemDto == null)
            throw new ArgumentNullException("DTO медикаменту = null");

        this.itemDto = itemDto;
    }

    public override string Name => "Створення медикаменту";

    public override bool Execute()
    {
        var item = mapper.Map<Item>(itemDto);
        dAPoint.ItemRepository.Add(item);
        dAPoint.Save();

        return true;
    }
}
