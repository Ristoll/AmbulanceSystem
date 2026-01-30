using Ambulance.BLL.Commands.BigadeCommands;
using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using AutoMapper.Execution;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Commands.ItemCommands
{
    public class LoadBrigadeItemsCommand : AbstrCommandWithDA<List<BrigadeItemDto>>
    {
        private readonly int brigadeId;

        public override string Name => "Завантаження медикаментів бригади";

        public LoadBrigadeItemsCommand(int brigadeId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.brigadeId = brigadeId;
        }

        public override List<BrigadeItemDto> Execute()
        {
            var brigadeItems = dAPoint.BrigadeItemRepository
                .GetAll()
                .Where(x => x.BrigadeId == brigadeId)
                .ToList();

            var brigadeItemsDtos = brigadeItems.Select(i =>
            {
                var dto = mapper.Map<BrigadeItemDto>(i);

                var item = dAPoint.ItemRepository.GetById(i.ItemId);
                if (item != null)
                {
                    dto.ItemName = item.Name;
                    dto.UnitType = item.UnitType;
                    dto.ItemType = item.ItemType;
                }

                return dto;
            }).ToList();

            return brigadeItemsDtos;
        }
    }
}