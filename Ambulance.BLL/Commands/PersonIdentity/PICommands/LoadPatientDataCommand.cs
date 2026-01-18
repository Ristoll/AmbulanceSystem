using Ambulance.Core.Entities;
using Ambulance.Core.Entities.StandartEnums;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

internal class LoadPatientDataCommand : AbstrCommandWithDA<PatientDto>
{
    private readonly int personId;

    public LoadPatientDataCommand(int personId, IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
        this.personId = personId;
    }

    public override string Name => "Отримання даних пацієнита";

    public override PatientDto Execute()
    {
        // персона з усіма пов'язаними даними
        var person = dAPoint.PersonRepository
            .GetQueryable()
            .Include(p => p.MedicalCard)
                .ThenInclude(mc => mc.PatientAllergies)
                    .ThenInclude(pa => pa.Allergy)
            .Include(p => p.MedicalCard)
                .ThenInclude(mc => mc.PatientChronicDeceases)
                    .ThenInclude(pcd => pcd.ChronicDecease)
            .Include(p => p.MedicalCard)
                .ThenInclude(mc => mc.MedicalRecords)
                    .ThenInclude(mr => mr.BrigadeMember)
                        .ThenInclude(bm => bm.Person)
            .FirstOrDefault(p => p.PersonId == personId && p.UserRole == UserRole.Patient.ToString());

        if (person == null)
            throw new InvalidOperationException("Пацієнта не знайдено");

        // через складність, виклики додаємо окремо
        var calls = dAPoint.CallRepository
            .GetQueryable()
            .Include(x => x.Patient)
            .Include(x => x.Dispatcher)
            .Include(x => x.Hospital)
            .Include(x => x.Brigades)
               .ThenInclude(x => x.BrigadeType)
            .Where(x => x.PatientId == person.PersonId)
            .OrderByDescending(x => x.StartCallTime); // для фронтенду найновіші зверху

        var dto = mapper.Map<PatientDto>(person);

        dto.Calls = mapper.Map<List<CallDto>>(calls.ToList()); // щоб не ламати еф трекінг

        return dto;
    }
}