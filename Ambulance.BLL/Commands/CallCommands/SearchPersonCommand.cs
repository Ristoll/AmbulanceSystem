using Ambulance.Core;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallCommands
{
    internal class SearchPersonCommand : AbstrCommandWithDA<PersonProfileDTO?>
    {
        private readonly PersonProfileDTO personProfileDTO;
        public SearchPersonCommand(PersonProfileDTO personProfileDTO, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            : base(operateUnitOfWork, mapper)
        {
            this.personProfileDTO = personProfileDTO;
        }

        public override string Name => "Пошук людини";

        public override PersonProfileDTO? Execute()
        {
            var person = dAPoint.PersonRepository.GetAll()
                .FirstOrDefault(p =>
                    string.Equals(p.Name, personProfileDTO.Name, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.Surname, personProfileDTO.Surname, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.MiddleName, personProfileDTO.MiddleName, StringComparison.OrdinalIgnoreCase) &&
                    p.DateOfBirth.HasValue && p.DateOfBirth.Value == personProfileDTO.DateOfBirth);

            if (person == null)
                return null;

            var result = mapper.Map<PersonProfileDTO>(person);
            return result;
        }

    }
}
