using Ambulance.Core;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Linq;

namespace Ambulance.BLL.Commands.CallsCommands
{
    public class SearchMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly int personId;

        public SearchMedicalCardCommand(int personId, IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext) 
            :base(operateUnitOfWork, mapper, userContext)
        {
            this.personId = personId;
        }

        public override string Name => "Пошук медичної картки";

        public override bool Execute()
        {
            var medicalCard = dAPoint.MedicalCardRepository
                .FirstOrDefault(mc => mc.PersonId == personId);
            if (medicalCard != null)
            {
                LogAction($"{Name}: Медична картка знайдена для особи з ID {personId}");
                return true;
            }
            else
            {
                LogAction($"{Name}: Медична картка не знайдена для особи з ID {personId}");
                return false;
            }
        }
    }
}
