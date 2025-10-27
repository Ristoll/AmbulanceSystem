using Ambulance.BLL.Commands.CallsCommands;
using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.BLL.Models;
using Ambulance.Core;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallCommands;
public class FillCallCommand : AbstrCommandWithDA<bool>
{
    private readonly PersonCreateModel personCreateModel;
    private readonly CallModel callModel;
    public override string Name => "Заповнення виклику";
    public FillCallCommand(PersonCreateModel personCreateModel, CallModel callModel, IUnitOfWork unitOfWork, IMapper mapper, IUserContext userContext)
        : base(unitOfWork, mapper, userContext)
    {
        this.callModel = callModel;
    }

    public override bool Execute()
    {
        var call = dAPoint.CallRepository.GetById(callModel.CallId);
        if (call == null)
        {
            LogAction($"{Name}: виклик з ID {callModel.CallId} не знайдено");
            return false;
        }

        // Перевірка пацієнта
        var patient = dAPoint.PersonRepository.GetById(callModel.PatientId);
        if (patient == null)
        {
            var createPersonCommand = new CreatePersonCommand(dAPoint, mapper, personCreateModel, userContext);
            createPersonCommand.Execute();
            
            patient = dAPoint.PersonRepository.FirstOrDefault(p => p.Login == personCreateModel.Login);

            if (patient == null)
                throw new InvalidOperationException("Не вдалося створити пацієнта");
        }
        var patientModel = mapper.Map<PersonExtModel>(patient);
        // Перевірка медкарти
        var medCard = dAPoint.MedicalCardRepository.GetById(patient.PersonId);
        if (medCard == null)
        {
            var createMedCardCommand = new CreateMedicalCardCommand(patientModel, dAPoint, mapper, userContext);
            createMedCardCommand.Execute();
        }

        // Заповнення виклику
        call.EndCallTime = DateTime.Now;
        call.Notes = callModel.Notes;
        call.DispatcherId = callModel.DispatcherId;
        call.PatientId = patient.PersonId;
        call.HospitalId = callModel.HospitalId;
        call.UrgencyType = callModel.UrgencyType;
        call.Phone = callModel.Phone;
        call.Address = callModel.Address;

        dAPoint.CallRepository.Update(call);
        dAPoint.Save();

        LogAction($"{Name} № {call.CallId}");
        return true;
    }


}