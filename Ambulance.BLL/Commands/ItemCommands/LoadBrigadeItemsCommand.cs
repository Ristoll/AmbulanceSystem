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
    public class LoadBrigadeItemsCommand : AbstrCommandWithDA<List<BrigadeItemDto>>
    {
        public override string Name => "Завантаження медикаментів";
        public LoadBrigadeItemsCommand(IUnitOfWork unitOfWork, IMapper mapper, )
            : base(unitOfWork, mapper)
        {
        }
        public override List<BrigadeItemDto> Execute()
        {
            var items = dAPoint.BrigadeItemRepository.GetAll().ToList();
            return mapper.Map<List<BrigadeItemDto>>(items);
        }
    }
}