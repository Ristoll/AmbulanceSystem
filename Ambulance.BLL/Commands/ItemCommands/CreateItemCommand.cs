using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core.Entities;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class CreateItemCommand : AbstrCommandWithDA<bool>
    {
        private readonly ItemDto itemDto;
        private readonly int actorId;

        public CreateItemCommand(ItemDto itemDto, int actorId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.itemDto = itemDto;
            this.actorId = actorId;
        }

        public override string Name => "Створення медикаменту";

        public override bool Execute()
        {
            var item = mapper.Map<Item>(itemDto);
            dAPoint.ItemRepository.Add(item);
            dAPoint.Save();
            LogAction($"{Name}", actorId);
            return true;
        }
    }
}
