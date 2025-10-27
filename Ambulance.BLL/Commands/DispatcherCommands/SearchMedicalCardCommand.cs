using AmbulanceSystem.Core.Data;
using AutoMapper;
using System;
using System.Linq;

namespace Ambulance.BLL.Commands.CallsCommands
{
    public class SearchMedicalCardCommand : AbstrCommandWithDA<bool>
    {
        private readonly string firstName;
        private readonly string lastName;
        private readonly string middleName;
        private readonly DateOnly birthDate;

        public SearchMedicalCardCommand(string firstName, string lastName, string middleName, DateOnly birthDate, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            :base(operateUnitOfWork, mapper)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.birthDate = birthDate;
        }

        public override string Name => "Пошук медичної картки";

        public override bool Execute()
        {
            var patient = dAPoint.PersonRepository.GetAll()
                .FirstOrDefault(p =>
                    p.Name.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                    p.Surname.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                    p.MiddleName.Equals(middleName, StringComparison.OrdinalIgnoreCase) &&
                    p.DateOfBirth == birthDate);

            return patient != null;
        }
    }
}
