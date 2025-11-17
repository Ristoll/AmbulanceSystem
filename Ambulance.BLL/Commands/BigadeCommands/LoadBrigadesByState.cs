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
    public class LoadBrigadesByStateCommand : AbstrCommandWithDA<List<BrigadeDto>>
    {
        public int brigadeStateId;
        public override string Name => "Підвантаження всіх бригад за станом";
        public LoadBrigadesByStateCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }
        public override List<BrigadeDto> Execute()
        {
            var brigades = dAPoint.BrigadeRepository.GetAll();
            brigades = brigades.Where(b => b.BrigadeStateId == brigadeStateId).ToList();
            return mapper.Map<List<BrigadeDto>>(brigades);
        }

    }
}
