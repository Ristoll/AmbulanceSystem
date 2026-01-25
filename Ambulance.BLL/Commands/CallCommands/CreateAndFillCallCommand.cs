using Ambulance.BLL.Commands.BigadeCommands;
using Ambulance.BLL.Commands.MedicalCardCommands;
using Ambulance.BLL.Commands.PersonIdentity;
using Ambulance.Core;
using Ambulance.Core.Entities;
using Ambulance.Core.Entities.StandartEnums;
using Ambulance.DTO;
using Ambulance.DTO.PersonModels;
using Ambulance.ExternalServices;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Linq;

namespace Ambulance.BLL.Commands.CallCommands;

public class CreateAndFillCallCommand : AbstrCommandWithDA<int>
{
    public override string Name => "Створення та заповнення виклику";

    private readonly CallDto callDto;
    private readonly PatientCreateRequest? newPerson;

    public CreateAndFillCallCommand(
        CallDto callDto,
        PatientCreateRequest? newPerson,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.callDto = callDto;
        this.newPerson = newPerson;
    }

    public override int Execute()
    {
        Person? patient = null;

        if (callDto.PatientId.HasValue)
        {
            patient = dAPoint.PersonRepository.GetById(callDto.PatientId.Value);

            if (patient == null)
                throw new InvalidOperationException($"Пацієнт з ID {callDto.PatientId.Value} не знайдено");

            if (patient.UserRole != UserRole.Patient.ToString())
                throw new InvalidOperationException("Знайдена чи створена людина не пацієнт");
        }

        if (patient == null && newPerson != null)
        {
            var newPatient = new Person()
            {
                Name = newPerson.Name,
                Surname = newPerson.Surname,
                MiddleName = newPerson.MiddleName,
                PhoneNumber = newPerson.PhoneNumber,
                Gender = newPerson.Gender,
                PasswordHash = PasswordHasher.HashPassword(newPerson.PhoneNumber), // встановлюємо пароль як номер телефону тимчасово
                UserRole = UserRole.Patient.ToString()
            };

            dAPoint.PersonRepository.Add(newPatient);
            dAPoint.Save();

            patient = dAPoint.PersonRepository
                .FirstOrDefault(p => p.PhoneNumber == newPatient.PhoneNumber // отримуємо доданого за нмоером телефону, бо віну нікальний
                  && p.UserRole == UserRole.Patient.ToString());
        }
        else if (patient == null)
        {
            // fallback якщо не передали ні ID ні дані
            var fallbackPatient = new Person()
            {
                PhoneNumber = callDto.Phone,
                UserRole = UserRole.Patient.ToString()
            };

            dAPoint.PersonRepository.Add(fallbackPatient);
            dAPoint.Save();

            patient = dAPoint.PersonRepository
                .FirstOrDefault(p => p.PhoneNumber == fallbackPatient.PhoneNumber
                  && p.UserRole == UserRole.Patient.ToString());
        }

        if (patient == null || patient.PersonId <= 0)
        {
            throw new InvalidOperationException("Помилка: пацієнт не був коректно створений у базі");
        }

        Console.WriteLine("DEBUG PatientId " + patient.PersonId);

        // створюємо медкартку, якщо її не існує
        var medCard = dAPoint.PersonRepository.GetById(patient.PersonId)?
            .Card;
        if (medCard == null)
        {
            var newCard = new MedicalCard
            {
                CreationDate = DateOnly.FromDateTime(DateTime.Now)
            };

            dAPoint.MedicalCardRepository.Add(newCard);
        }

        var call = new Call
        {
            CallAt = callDto.StartCallTime,
            Notes = callDto.Notes,
            DispatcherId = callDto.DispatcherId,
            UrgencyType = callDto.UrgencyType,
            PhoneNumber = callDto.Phone,
            CallAddress = callDto.Address,
            PersonId = patient.PersonId
        };

        dAPoint.CallRepository.Add(call);
        dAPoint.Save();

        //і під кінець оновлюємо статус бригади, якщо вона була призначена
        if (callDto.AssignedBrigades != null)
        {
            foreach (var br in callDto.AssignedBrigades)
            {
                var brigade = dAPoint.BrigadeRepository.GetById(br.BrigadeId);
                brigade.CurrentCallId = call.CallId;
                brigade.BrigadeState = BrigadeState.OnTheCall.ToString();
                dAPoint.BrigadeRepository.Update(brigade);
            }
        }
        dAPoint.Save();

        return call.CallId;
    }
}
