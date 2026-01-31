using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;

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
            var medicalCards = dAPoint.MedicalCardRepository.GetAll();

            return mapper.Map<List<MedicalCardDto>>(medicalCards);
        }
    }
}