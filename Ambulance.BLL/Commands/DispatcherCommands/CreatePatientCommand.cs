using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallsCommands;

public class CreatePatientCommand : AbstrCommandWithDA<bool>
{
    private readonly PersonModel personModel;
    public CreatePatientCommand(PersonModel personModel, IUnitOfWork operateUnitOfWork, IMapper mapper) 
        :base(operateUnitOfWork, mapper)
    {
        this.personModel = personModel;
    }
    public override string Name => "Додавання пацієнта в БД";
    public override bool Execute()
    {
        var patientModel = new PatientModel
        {
            PersonId = personModel.PersonId
        };

        var patient = mapper.Map<Patient>(personModel);
        dAPoint.PatientRepository.Add(patient);
        dAPoint.Save();
        string patientName = $"{personModel.Surname} {personModel.Name} {personModel.MiddleName}";
        LogAction($"{Name} з ім'ям {patientName}");
        return true;
    }
}
