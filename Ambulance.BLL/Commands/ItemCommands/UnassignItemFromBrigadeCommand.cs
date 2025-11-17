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
    public class UnassignItemFromBrigadeCommand : AbstrCommandWithDA<bool>
    {
        private readonly int brigadeItemId;

        public UnassignItemFromBrigadeCommand(int brigadeItemId, IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper)
        {
            this.brigadeItemId = brigadeItemId;
        }

        public override string Name => "Винесення медикамента з бригади";

        public override bool Execute()
        {
            var brigadeItem = dAPoint.BrigadeItemRepository
                .FirstOrDefault(bi => bi.BrigadeItemId == brigadeItemId);

            dAPoint.BrigadeItemRepository.Remove(brigadeItemId);
            dAPoint.Save();

            return true;
        }
    }
}
