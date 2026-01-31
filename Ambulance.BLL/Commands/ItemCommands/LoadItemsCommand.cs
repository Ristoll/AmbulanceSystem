using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.ItemCommands;

public class LoadItemsCommand : AbstrCommandWithDA<List<ItemDto>>
{
    public override string Name => "Завантаження медикаментів";

    public LoadItemsCommand(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
    }

    public override List<ItemDto> Execute()
    {
        var items = dAPoint.ItemRepository.GetAll();

        return mapper.Map<List<ItemDto>>(items);
    }
}
