using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Ambulance.BLL.Commands.BigadeCommands
//{
//    public class LoadAllBrigadesCommand : AbstrCommandWithDA<List<BrigadeDto>>
//    {
//        public override string Name => "Підвантаження всіх бригад";

//        public LoadAllBrigadesCommand(IUnitOfWork unitOfWork, IMapper mapper)
//            : base(unitOfWork, mapper)
//        {
//        }

//        public override List<BrigadeDto> Execute()
//        {
//            // Беремо всі бригади
//            var brigades = dAPoint.BrigadeRepository.GetAll();

//            // Мапимо на DTO
//            var brigadeDtos = brigades.Select(b =>
//            {
//                var dto = mapper.Map<BrigadeDto>(b);

//                // Підтягуємо тип бригади
//                var type = dAPoint.BrigadeTypeRepository.GetById(b.BrigadeTypeId);
//                dto.BrigadeTypeName = type != null ? type.Name : "Не вказано";

//                return dto;
//            }).ToList();

//            return brigadeDtos;
//        }
//    }
//}
