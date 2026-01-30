using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Linq;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    public class SearchMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly int cardId;

        public SearchMedicalCardCommand(int cardId, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            :base(operateUnitOfWork, mapper)
        {
            this.cardId = cardId;
        }

        public override string Name => "Пошук медичної картки";

        public override bool Execute()
        {
            var medicalCard = dAPoint.PersonRepository
                .FirstOrDefault(mc => mc.CardId == cardId);
            if (medicalCard != null)
            {
                return true;
            }
            else
            {
                throw new ArgumentNullException($"{Name}: Медична картка не знайдена");
            }
        }
    }
}
