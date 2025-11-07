using Ambulance.BLL.Commands.CallsCommands;
using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.Core;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Linq;

namespace Ambulance.BLL.Commands.CallCommands;

public class FillCallCommand : AbstrCommandWithDA<bool>
{
    private readonly CallDto callDto;
    private readonly PatientDto? patientDto;
    private readonly PersonCreateRequest personCreateRequest;
    private readonly int actorId;

    public override string Name => "Заповнення виклику";

    public FillCallCommand(CallDto callDto, PatientDto? patientDto, int actorId, PersonCreateRequest personCreateRequest, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.callDto = callDto;
        this.patientDto = patientDto;
        this.actorId = actorId;
        this.personCreateRequest = personCreateRequest;
    }

    public override bool Execute()
    {
        //Отримуємо виклик
        var call = dAPoint.CallRepository.GetById(callDto.CallId)
            ?? throw new InvalidOperationException($"Виклик з ID {callDto.CallId} не знайдено");

        Person? patient = null;

        // Якщо передано DTO пацієнта — шукаємо або створюємо
        if (patientDto != null)
        {
            // Викликаємо команду пошуку
            var searchCommand = new SearchPersonCommand(patientDto, dAPoint, mapper);

            var foundPerson = searchCommand.Execute();

            if (foundPerson != null)
            {
                // Якщо знайдено — отримуємо саму сутність
                patient = dAPoint.PersonRepository.GetAll().FirstOrDefault(p =>
                    string.Equals(p.Name, patientDto.Name, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.Surname, patientDto.Surname, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.MiddleName, patientDto.MiddleName, StringComparison.OrdinalIgnoreCase) &&
                    p.DateOfBirth.HasValue && p.DateOfBirth.Value == patientDto.DateOfBirth);
            }
            else
            {
                // Якщо не знайдено — створюємо нову особу
                var createPersonCommand = new CreatePersonCommand(dAPoint, mapper, personCreateRequest, actorId);
                createPersonCommand.Execute();

                patient = dAPoint.PersonRepository.GetAll().FirstOrDefault(p =>
                    string.Equals(p.Name, patientDto.Name, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.Surname, patientDto.Surname, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.MiddleName, patientDto.MiddleName, StringComparison.OrdinalIgnoreCase) &&
                    p.DateOfBirth.HasValue && p.DateOfBirth.Value == patientDto.DateOfBirth)
                    ?? throw new InvalidOperationException("Не вдалося створити пацієнта");
            }
        }

        if (patient == null)
            throw new InvalidOperationException("Пацієнта не знайдено або не передано");

        // Перевіряємо, чи є медична картка
        var medCard = dAPoint.MedicalCardRepository.GetById(patient.PersonId);
        if (medCard == null)
        {
            var createMedCardCommand = new CreateMedicalCardCommand(patient.PersonId, actorId, dAPoint, mapper);
            createMedCardCommand.Execute();
        }

        // Оновлюємо виклик
        call.PatientId = patient.PersonId;
        call.EndCallTime = DateTime.Now;
        call.Notes = callDto.Notes;
        call.DispatcherId = callDto.DispatcherId;
        call.UrgencyType = callDto.UrgencyType;
        call.Phone = callDto.Phone;
        call.Address = callDto.Address;

        dAPoint.CallRepository.Update(call);
        dAPoint.Save();

        LogAction($"{Name} № {call.CallId}", actorId);
        return true;
    }
}
