using AutoMapper;
using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using Microsoft.EntityFrameworkCore;
using Ambulance.DTO.PersonModels;
using Ambulance.Core.Entities.StandartEnums;

namespace Ambulance.BLL.Commands.PersonIdentity.PICommands;

internal class LoadPatientDataCommand : AbstrCommandWithDA<PatientDto>
{
    private readonly int personId;

    public LoadPatientDataCommand(int personId, IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
        this.personId = personId;
    }

    public override string Name => "Отримання даних пацієнта";

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
            .Include(x => x.Person)
            .Include(x => x.Dispatcher)
            .Include(x => x.Hospital)
            .Where(x => x.PersonId == person.PersonId);

        var dto = mapper.Map<PatientDto>(person);

        dto.Calls = mapper.Map<List<CallDto>>(calls.ToList()); // щоб не ламати еф трекінг

        return dto;
    }
}