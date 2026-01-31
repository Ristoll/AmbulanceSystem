using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;

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
            var brigade = dAPoint.BrigadeRepository.GetById(brigadeDto.BrigadeId);

            if (brigade == null)
                throw new Exception("Brigade not found");

            mapper.Map(brigadeDto, brigade);
            dAPoint.BrigadeRepository.Update(brigade);
            dAPoint.Save();

            return mapper.Map<BrigadeDto>(brigade);
        }
    }

}
