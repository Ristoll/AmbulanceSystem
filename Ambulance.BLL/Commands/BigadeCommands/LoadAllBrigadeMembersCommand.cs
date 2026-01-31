using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace Ambulance.BLL.Commands.BigadeCommands
{
    public class LoadAllBrigadeMembersCommand : AbstrCommandWithDA<List<BrigadeMemberDto>>
   {
        private readonly int brigadeId;
        public override string Name => "Завантаження всіх членів бригади";

        public LoadAllBrigadeMembersCommand(int brigadeId,IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.brigadeId = brigadeId;
        }

       public override List<BrigadeMemberDto> Execute()
        {
            // Беремо всіх членів бригад
            var members = dAPoint.BrigadeMemberRepository.GetAll()
                .Where(m => m.BrigadeId == brigadeId)
                .ToList();

            var memberDtos = members.Select(m =>
            {
                var dto = mapper.Map<BrigadeMemberDto>(m);

<<<<<<< HEAD
//                // Підтягуємо роль
//                var role = dAPoint.BrigadeMemberRoleRepository.GetById(m.BrigadeMemberRoleId);
//                dto.RoleName = role != null ? role.Name : "Не вказано";

//                // Підтягуємо спеціалізацію
//                var specialization = dAPoint.BrigadeMemberSpecializationTypeRepository.GetById(m.MemberSpecializationTypeId);
//                dto.SpecializationTypeName = specialization != null ? specialization.Name : "Не вказано";
//                var person = dAPoint.PersonRepository.GetById(m.PersonId);
//                dto.PersonFullName = person != null ? $"{person.Surname} {person.Name} {person.MiddleName}" : "Не вказано";
//                return dto;
//            }).ToList();

//            return memberDtos;
//        }
//    }
//}
=======
                var specialization = dAPoint.SpecializationTypeRepository.GetById(m.SpecializationTypeId);
                dto.SpecializationTypeName = specialization != null ? specialization.Name : "Не вказано";
                var person = dAPoint.PersonRepository.GetById(m.PersonId);
                dto.PersonFullName = person != null ? $"{person.Surname} {person.Name} {person.MiddleName}" : "Не вказано";
                return dto;
            }).ToList();

            return memberDtos;
        }
    }
}
>>>>>>> 2b9d3932
