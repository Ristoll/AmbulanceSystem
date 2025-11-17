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
        public int brigadeStateId;
        public BrigadeDto brigade;
        public override string Name => "Зміна стану бригади";
        public ChangeBrigadeStateCommand(int brigadeStateId, BrigadeDto brigade, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.brigadeStateId = brigadeStateId;
            this.brigade = brigade;
        }
        public override bool Execute()
        {
            var brigadeRepo = dAPoint.BrigadeRepository;
            var brigadeEntity = brigadeRepo.GetById(brigade.BrigadeId);
            if (brigadeEntity == null)
                throw new Exception("Brigade not found");
            brigadeEntity.BrigadeStateId = brigadeStateId;
            brigadeRepo.Update(brigadeEntity);
            dAPoint.Save();
            return true;
        }
    }
}
