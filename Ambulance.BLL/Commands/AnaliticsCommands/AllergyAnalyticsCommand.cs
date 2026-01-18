using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Commands.AnaliticsCommands
{
    public class AllergyAnalyticsCommand : AbstrCommandWithDA<Dictionary<string, int>>
    {
        public override string Name => "Аналітика алергій";

        public AllergyAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper) { }

        public override Dictionary<string, int> Execute()
        {
            // Беремо всі записи пацієнтів з алергіями
            var patientAllergies = dAPoint.PatientAllergyRepository
                .GetAll()
                .ToList();

            // Дістаємо всі алергії для lookup по ID
            var allergiesLookup = dAPoint.AllergyRepository
                .GetAll()
                .ToDictionary(a => a.AllergyId, a => a.Name);

            // Групуємо по назві алергії і рахуємо кількість пацієнтів
            var result = patientAllergies
                .GroupBy(pa => allergiesLookup.ContainsKey(pa.AllergyId)
                                ? allergiesLookup[pa.AllergyId]
                                : "Unknown")
                .ToDictionary(
                    g => g.Key,
                    g => g.Count(),
                    StringComparer.OrdinalIgnoreCase
                );

            return result;
        }
    }
}

