using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities;

namespace Ambulance.BLL.Commands.ItemCommands;

public class AssignItemToBrigadeCommand : AbstrCommandWithDA<bool>
{
    private readonly BrigadeItemDto brigadeItemDto;

    public AssignItemToBrigadeCommand(BrigadeItemDto brigadeItemDto, IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
        if (brigadeItemDto == null)
            throw new ArgumentNullException("DTO медикаменту бригади (проміжна таблиця) = null");

        this.brigadeItemDto = brigadeItemDto;
    }

    public override string Name => "Внесення медикамента до бригади";

    public override bool Execute()
    {
        var brigadeItem = mapper.Map<BrigadeItem>(brigadeItemDto);
        dAPoint.BrigadeItemRepository.Add(brigadeItem);
        dAPoint.Save();

        return true;
    }
}
