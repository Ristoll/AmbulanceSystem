using AutoMapper;
using Ambulance.DTO;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using Ambulance.ExternalServices;
using Ambulance.Core.Entities;
using Ambulance.Core.Entities.StandartEnums;

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
        ArgumentNullException.ThrowIfNull(callDto, "DTO дзвінка = null");

        if(callDto.PatientId.HasValue && newPerson != null)
            throw new ArgumentException("Не можна одночасно передавати і ID пацієнта, і дані для створення нового пацієнта");

        this.callDto = callDto;
        this.newPerson = newPerson;
    }

    public override int Execute()
    {
        Person? patient = null;

        if (callDto.PatientId.HasValue)
        {
            patient = dAPoint.PersonRepository.GetById(callDto.PatientId.Value);

            if (patient.UserRole != UserRole.Patient.ToString()) // запитати у Крістіни щодо гнучкості, можливо прибрати
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
                .FirstOrDefault(p => p.PhoneNumber == newPatient.PhoneNumber // отримуємо доданого за номером телефону, бо він унікальний
                  && p.UserRole == UserRole.Patient.ToString()); // запитати у Крістіни щодо гнучкості, можливо прибрати
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
            throw new InvalidOperationException("Помилка: пацієнт не був коректно створений у базі");

        // створюємо медкартку, якщо її не існує
        var medCard = dAPoint.MedicalCardRepository.FirstOrDefault(mc => mc.PatientId == patient.PersonId);

        if (medCard == null)
        {
            var newCard = new MedicalCard
            {
                PatientId = patient.PersonId,
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                DateOfBirth = callDto.DateOfBirth != null ? DateOnly.FromDateTime(callDto.DateOfBirth.Value) : DateOnly.MinValue
            };

            dAPoint.MedicalCardRepository.Add(newCard);
        }

        var call = new Call
        {
            CallAt = callDto.StartCallTime,
            HospitalId = callDto.HospitalId,
            DispatcherId = callDto.DispatcherId,
            UrgencyType = ((UrgencyType)callDto.UrgencyType).ToString(),
            CallAddress = callDto.Address,
            PersonId = patient.PersonId,
            CallState = CallState.New.ToString()
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
