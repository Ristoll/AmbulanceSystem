using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.BigadeCommands
{
    public class LoadBrigadeMemberCommand : AbstrCommandWithDA<BrigadeMemberDto>
    {
        public int memberId { get; }
        public override string Name => "Завантаження одного члена бригади";

        public LoadBrigadeMemberCommand(int memberId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.memberId = memberId;
        }

        public override BrigadeMemberDto Execute()
        {
            // Беремо члена бригади по ID
            var member = dAPoint.BrigadeMemberRepository.GetById(memberId);
            if (member == null)
                throw new Exception($"Член бригади з ID {memberId} не знайдений");

            // Мапимо на DTO
            var memberDto = mapper.Map<BrigadeMemberDto>(member);
            // Підтягуємо спеціалізацію
            var specialization = dAPoint.SpecializationTypeRepository.GetById(member.SpecializationTypeId);
            memberDto.SpecializationTypeName = specialization != null ? specialization.Name : "Не вказано";
            var person = dAPoint.PersonRepository.GetById(member.PersonId);
            memberDto.PersonFullName = person != null ? $"{person.Surname} {person.Name} {person.MiddleName}" : "Не вказано";
            return memberDto;
        }
    }
}
