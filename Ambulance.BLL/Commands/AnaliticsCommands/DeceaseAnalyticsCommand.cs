using AutoMapper;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.AnaliticsCommands
{
    public class DeceaseAnalyticsCommand : AbstrCommandWithDA<Dictionary<string, int>>
    {
        public override string Name => "Аналітика хронічних захворювань";

        public DeceaseAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper) { }

        public override Dictionary<string, int> Execute()
        {
            // Беремо всі зв'язки пацієнтів з хронічними захворюваннями
            var patientDiseases = dAPoint.PatientChronicDeceaseRepository
                .GetAll()
                .ToList();

            // Дістаємо всі хронічні захворювання для lookup по ID
            var diseasesLookup = dAPoint.ChronicDeceaseRepository
                .GetAll()
                .ToDictionary(d => d.ChronicDeceaseId, d => d.Name);

            // Групуємо по назві захворювання і рахуємо кількість пацієнтів
            var result = patientDiseases
                .GroupBy(pcd => diseasesLookup.ContainsKey(pcd.ChronicDeceaseId)
                                ? diseasesLookup[pcd.ChronicDeceaseId]
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


