using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class AssignItemToBrigadeCommand : AbstrCommandWithDA<bool>
    {
        private readonly BrigadeItemDto brigadeItemDto;
        private readonly int actorId;

        public AssignItemToBrigadeCommand(BrigadeItemDto brigadeItemDto, int actorId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.brigadeItemDto = brigadeItemDto;
            this.actorId = actorId;
        }

        public override string Name => "Внесення медикамента до бригади";

        public override bool Execute()
        {
            var brigadeItem = mapper.Map<BrigadeItem>(brigadeItemDto);
            dAPoint.BrigadeItemRepository.Add(brigadeItem);
            dAPoint.Save();
            LogAction($"{Name} з ID {brigadeItem.BrigadeId}", actorId);
            return true;
        }
    }
}
