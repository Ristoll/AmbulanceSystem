using Ambulance.Core.Entities;
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
    public class AssignItemToBrigadeCommand : AbstrCommandWithDA<bool>
    {
        private readonly BrigadeItemDto brigadeItemDto;

        public AssignItemToBrigadeCommand(BrigadeItemDto brigadeItemDto, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
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
}
