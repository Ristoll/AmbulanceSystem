using Ambulance.Core.Entities.StandartEnums;
using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.BigadeCommands
{
    public class ChangeBrigadeStateCommand : AbstrCommandWithDA<bool>
    {
        public string stateName;
        public int brigadeId;
        public override string Name => "Зміна стану бригади";
        public ChangeBrigadeStateCommand(string stateName, int brigadeId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.stateName = stateName;
            this.brigadeId = brigadeId;
        }
        public override bool Execute()
        {
            if(Enum.TryParse<BrigadeState>(stateName, out var brigadeStateEnum))
            {
                throw new Exception("Нема такого стану");
            }

            var brigadeRepo = dAPoint.BrigadeRepository;
            var brigadeEntity = brigadeRepo.GetById(brigadeId);

            if (brigadeEntity == null)
                throw new Exception("Нема такої бригади");

            brigadeEntity.BrigadeState = stateName;
            brigadeRepo.Update(brigadeEntity);
            dAPoint.Save();
            return true;
        }
    }
}
