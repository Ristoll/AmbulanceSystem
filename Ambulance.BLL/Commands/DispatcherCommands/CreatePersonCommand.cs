using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallsCommands
{
    public class CreatePersonCommand : AbstrCommandWithDA<bool>
    {
        private readonly PersonModel personModel;

        public CreatePersonCommand(PersonModel personModel, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            :base(operateUnitOfWork, mapper)
        {
            this.personModel = personModel;
        }

        public override string Name => "Додавання нової людини в БД";

        public override bool Execute()
        {
            var person = mapper.Map<Person>(personModel);
            dAPoint.PersonRepository.Add(person);
            dAPoint.Save();
            string personName = $"{person.Surname} {person.Name} {person.MiddleName}";
            LogAction($"{Name} з ім'ям {personName}");
            return true;
        }
    }
}
