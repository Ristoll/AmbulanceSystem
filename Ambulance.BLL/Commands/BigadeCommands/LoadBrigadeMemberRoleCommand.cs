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
    public class LoadBrigadeMemberRoleCommand : AbstrCommandWithDA<string>
    {
        public int brigadeMemberId;
        public override string Name => "Завантаження спеціалізації члена бригади";
        public LoadBrigadeMemberRoleCommand(int brigadeMemberId, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.brigadeMemberId = brigadeMemberId;
        }
        public override string Execute()
        {
            var member = dAPoint.BrigadeMemberRepository.GetById(brigadeMemberId);
            var role = dAPoint.BrigadeMemberRoleRepository.GetById(member.MemberSpecializationTypeId);
            return role.Name;
        }

    }
}