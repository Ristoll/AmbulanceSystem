using Ambulance.Core;
using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallCommands
{
    internal class SearchPersonCommand : AbstrCommandWithDA<bool>
    {
        private readonly string firstName;
        private readonly string lastName;
        private readonly string middleName;
        private readonly DateOnly birthDate;
        public SearchPersonCommand(string firstName, string lastName, string middleName, DateOnly birthDate, IUnitOfWork operateUnitOfWork, IMapper mapper, IUserContext userContext) 
            : base(operateUnitOfWork, mapper, userContext)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.birthDate = birthDate;
        }

        public override string Name => "Пошук людини";

        public override bool Execute()
        {
            var person = dAPoint.PersonRepository.GetAll()
                .FirstOrDefault(p =>
                    string.Equals(p.Name, firstName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.Surname, lastName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.MiddleName, middleName, StringComparison.OrdinalIgnoreCase) &&
                    p.DateOfBirth.HasValue && p.DateOfBirth.Value == birthDate);
            return person != null;
        }
    }
}
