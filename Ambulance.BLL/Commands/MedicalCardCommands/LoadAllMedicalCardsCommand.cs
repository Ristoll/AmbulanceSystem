using Ambulance.Core;
using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    public class LoadAllMedicalCardsCommand : AbstrCommandWithDA<List<MedicalCardDto>>
    {
        public LoadAllMedicalCardsCommand(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }
        public override string Name => "Завантаження медичних записів";
        public override List<MedicalCardDto> Execute()
        {
            var medicalCards = dAPoint.MedicalCardRepository
                .GetAll()
                .ToList();
            return mapper.Map<List<MedicalCardDto>>(medicalCards);
        }

    }
}