using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Linq;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    public class SearchMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly int personId;

        public SearchMedicalCardCommand(int personId, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            :base(operateUnitOfWork, mapper)
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
                return true;
            }
            else
            {
                throw new ArgumentNullException($"{Name}: Медична картка не знайдена для особи з ID {personId}");
            }
        }
    }
}
