using AutoMapper;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.BigadeCommands
{
    public class LoadBrigadeMemberSpecialisationTypeCommand : AbstrCommandWithDA<string>
    {
        public int brigadeMemberId;
        public override string Name => "Завантаження спеціалізації члена бригади";
        public LoadBrigadeMemberSpecialisationTypeCommand(int brigadeMemberId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.brigadeMemberId = brigadeMemberId;
        }
        public override string Execute()
        {
            var member = dAPoint.BrigadeMemberRepository.GetById(brigadeMemberId);
            var specializationType = dAPoint.SpecializationTypeRepository.GetById(member.SpecializationTypeId);
            return specializationType.Name;
        }

    }
}
