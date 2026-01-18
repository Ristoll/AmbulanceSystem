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

            // Підтягуємо роль
            var role = dAPoint.BrigadeMemberRoleRepository.GetById(member.BrigadeMemberRoleId);
            memberDto.RoleName = role != null ? role.Name : "Не вказано";

            // Підтягуємо спеціалізацію
            var specialization = dAPoint.BrigadeMemberSpecializationTypeRepository.GetById(member.MemberSpecializationTypeId);
            memberDto.SpecializationTypeName = specialization != null ? specialization.Name : "Не вказано";
            var person = dAPoint.PersonRepository.GetById(member.PersonId);
            memberDto.PersonFullName = person != null ? $"{person.Surname} {person.Name} {person.MiddleName}" : "Не вказано";
            return memberDto;
        }
    }
}
