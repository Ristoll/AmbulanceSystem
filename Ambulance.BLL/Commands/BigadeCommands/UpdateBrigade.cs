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
    public class UpdateBrigade : AbstrCommandWithDA<BrigadeDto>
    {
        private readonly BrigadeDto brigadeDto;
        public override string Name => "Оновлення даних бригади";
        public UpdateBrigade(BrigadeDto brigadeDto, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.brigadeDto = brigadeDto;
        }

        public override BrigadeDto Execute()
        {
            var brigadeRepo = dAPoint.BrigadeRepository;
            var brigade = brigadeRepo.GetById(brigadeDto.BrigadeId);

            if (brigade == null)
                throw new Exception("Brigade not found");

            mapper.Map(brigadeDto, brigade);
            brigadeRepo.Update(brigade);
            dAPoint.Save();

            return mapper.Map<BrigadeDto>(brigade);
        }
    }

}
