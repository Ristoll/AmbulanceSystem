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
    public class LoadBrigadeTypeCommand : AbstrCommandWithDA<BrigadeTypeDto>
    {
        public int brigadeId;
        public override string Name => "Завантаження бригади";
        public LoadBrigadeTypeCommand(int brigadeId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.brigadeId = brigadeId;
        }
        public override BrigadeTypeDto Execute()
        {
            var brigade = dAPoint.BrigadeRepository.GetById(brigadeId);
            if (brigade == null)
                throw new Exception($"Бригада з ID {brigadeId} не знайдена");
            var brigadeType = dAPoint.BrigadeTypeRepository.GetById(brigade.BrigadeTypeId);
            return mapper.Map<BrigadeTypeDto>(brigadeType);
        }
    }
}
