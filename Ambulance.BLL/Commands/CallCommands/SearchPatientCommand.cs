using Ambulance.Core;
using Ambulance.Core.Entities;
using Ambulance.Core.Entities.StandartEnums;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallCommands;

internal class SearchPatientCommand : AbstrCommandWithDA<IEnumerable<PersonProfileDTO>>
{
    private readonly string textSearch;

    public SearchPatientCommand(string? textSearch, IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
        this.textSearch = textSearch?.Trim() ?? string.Empty;
    }

    public override string Name => "Пошук пацієнта";

    public override IEnumerable<PersonProfileDTO> Execute()
    {
        var query = dAPoint.PersonRepository.GetQueryable()
            .Where(p => p.UserRole == UserRole.Patient.ToString());

        if (string.IsNullOrEmpty(textSearch))
            return mapper.Map<IEnumerable<PersonProfileDTO>>(query);

        // розбиваємо на ключові слова
        var keywords = textSearch
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(k => k.Trim())
            .ToArray();

        // SQL-пошук по базових полях --- зробив для ефективності
        if (keywords.Length > 0)
        {
            query = query.Where(c => keywords.Any(k =>
            (c.Name != null && c.Name.Contains(k)) ||
                (c.Surname != null && c.Surname.Contains(k)) ||
                (c.MiddleName != null && c.MiddleName.Contains(k)) ||
                (c.PhoneNumber != null && c.PhoneNumber.Contains(k)) ||
                (c.Email != null && c.Email.Contains(k)) ||
                (c.Login != null && c.Login.Contains(k))));// бо коментарі - основа анонімних клієнтів
        }

        var patients = query.ToList();

        // якщо результатів мало — запускаємо рефлексію (здебільшого для майбутнього масштабувнаня)
        if (patients.Count < 100)
        {
            // беремо лише string-поля, крім пароля
            var props = typeof(Person)
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.Name != nameof(Person.PasswordHash))
                .ToList();

            var result = patients.Where(person =>
                keywords.Any(k =>
                    props.Any(p =>
                    {
                        var value = p.GetValue(person) as string;
                        return !string.IsNullOrEmpty(value) &&
                               value.Contains(k, StringComparison.OrdinalIgnoreCase);
                    })
                )
            );

            return mapper.Map<IEnumerable<PersonProfileDTO>>(result);
        }

        return mapper.Map<IEnumerable<PersonProfileDTO>>(patients);
    }
}
