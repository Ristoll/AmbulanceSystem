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
        public string stateName;
        public override string Name => "Підвантаження всіх бригад за станом";
        public LoadBrigadesByStateCommand(IUnitOfWork unitOfWork, IMapper mapper, string stateName)
            : base(unitOfWork, mapper)
        {
            this.stateName = stateName;
        }
        public override List<BrigadeDto> Execute()
        {
            var brigades = dAPoint.BrigadeRepository.GetQueryable()
                .Where(b => b.BrigadeState == stateName);

            return mapper.Map<List<BrigadeDto>>(brigades);
        }
    }
}
