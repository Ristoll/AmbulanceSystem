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
    public class LoadAllBrigadesCommand : AbstrCommandWithDA<List<BrigadeDto>>
    {
        public override string Name => "Підвантаження всіх бригад";
        public LoadAllBrigadesCommand(IUnitOfWork unitOfWork, IMapper mapper, int actionOwner)
            : base(unitOfWork, mapper, actionOwner)
        {
        }
        public override List<BrigadeDto> Execute()
        {
            var brigades = dAPoint.BrigadeRepository.GetAll();
            return mapper.Map<List<BrigadeDto>>(brigades);
        }

    }
}
