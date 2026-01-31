using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace Ambulance.BLL.Commands.BigadeCommands
{
    public class LoadAllBrigadesCommand : AbstrCommandWithDA<List<BrigadeDto>>
    {
        public override string Name => "Підвантаження всіх бригад";

        public LoadAllBrigadesCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public override List<BrigadeDto> Execute()
        {
            // Беремо всі бригади
           var brigades = dAPoint.BrigadeRepository.GetAll();

            // Мапимо на DTO
            var brigadeDtos = brigades.Select(b =>
            {
                var dto = mapper.Map<BrigadeDto>(b);

                // Підтягуємо тип бригади
                dto.BrigadeTypeName = b.BrigadeType.ToString();

                return dto;
           }).ToList();

           return brigadeDtos;
       }
    }
}
