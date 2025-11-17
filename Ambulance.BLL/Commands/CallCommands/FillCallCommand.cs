using Ambulance.BLL.Commands.MedicalCardCommands;
using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.Core;
using Ambulance.Core.Entities;
using Ambulance.DTO.PersonModels;
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

    public override string Name => "Заповнення виклику";

    public FillCallCommand(CallDto callDto, PatientDto? patientDto, PersonCreateRequest personCreateRequest, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.callDto = callDto;
        this.patientDto = patientDto;
        this.personCreateRequest = personCreateRequest;
    }

    public override bool Execute()
    {
        var call = dAPoint.CallRepository.GetById(callDto.CallId)
            ?? throw new InvalidOperationException($"Виклик з ID {callDto.CallId} не знайдено");

        Person? patient = null;

        if (patientDto != null)
        {
            var searchCommand = new SearchPersonCommand(patientDto, dAPoint, mapper);
            var foundPersonDto = searchCommand.Execute();

            if (foundPersonDto != null)
            {
                var foundPerson = mapper.Map<Person>(foundPersonDto);
                patient = foundPerson;
            }
            else
            {
                // Якщо не знайдено — створюємо нову особу
                var createPersonCommand = new CreatePersonCommand(dAPoint, mapper, personCreateRequest);
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

        // 3️⃣ Перевіряємо, чи є медична картка
        var medCard = dAPoint.MedicalCardRepository.FirstOrDefault(mc => mc.PersonId == patient.PersonId);
        if (medCard == null)
        {
            var medicalCardDto = new MedicalCardDto
            {
                PatientId = patient.PersonId,
                CreationDate = DateTime.Now
            };

            var createMedCardCommand = new CreateMedicalCardCommand(medicalCardDto, dAPoint, mapper);
            createMedCardCommand.Execute();
        }

        //Оновлюємо виклик
        call.PatientId = patient.PersonId;
        call.EndCallTime = DateTime.Now;
        call.Notes = callDto.Notes;
        call.DispatcherId = callDto.DispatcherId;
        call.UrgencyType = callDto.UrgencyType;
        call.Phone = callDto.Phone;
        call.Address = callDto.Address;

        dAPoint.CallRepository.Update(call);
        dAPoint.Save();

        return true;
    }
}
