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
        private readonly int actorId;

        public SearchMedicalCardCommand(int personId, int actorId, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            :base(operateUnitOfWork, mapper)
        {
            this.personId = personId;
            this.actorId = actorId;
        }

        public override string Name => "Пошук медичної картки";

        public override bool Execute()
        {
            var medicalCard = dAPoint.MedicalCardRepository
                .FirstOrDefault(mc => mc.PersonId == personId);
            if (medicalCard != null)
            {
                LogAction($"{Name}: Медична картка знайдена для особи з ID {personId}", actorId);
                return true;
            }
            else
            {
                LogAction($"{Name}: Медична картка не знайдена для особи з ID {personId}", actorId);
                return false;
            }
        }
    }
}
