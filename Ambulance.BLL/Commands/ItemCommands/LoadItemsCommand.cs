using Ambulance.Core;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class LoadItemsCommand : AbstrCommandWithDA<List<ItemDto>>
    {
        public override string Name => "Завантаження медикаментів";
        public LoadItemsCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }
        public override List<ItemDto> Execute()
        {
            var items = dAPoint.ItemRepository.GetAll().ToList();
            return mapper.Map<List<ItemDto>>(items);
        }
    }
}
