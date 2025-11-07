using Ambulance.BLL.Models;
using AmbulanceSystem.Core;
using Ambulance.DTO.PersonModels;
using AutoMapper;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

public class DeceaseAnalyticsCommand : AbstrCommandWithDA<List<DecAllergAnalyticsDTO>>
{
    public override string Name => "Аналітика хронічних захворювань та алергій";

    public DeceaseAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
    }

    public override List<DecAllergAnalyticsDTO> Execute()
    {
        // Отримуємо всіх пацієнтів
        var patients = dAPoint.PersonRepository.GetAll().Where(p => p.MedicalCards != null).Select(p => mapper.Map<PatientDto>(p)).ToList();

        var deceaseStats = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var allergyStats = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var patient in patients)
        {
            foreach (var disease in patient.ChronicDiseases.Distinct())
            {
                if (string.IsNullOrWhiteSpace(disease)) continue;
                if (deceaseStats.ContainsKey(disease))
                    deceaseStats[disease]++;
                else
                    deceaseStats[disease] = 1;
            }

            foreach (var allergy in patient.Allergies.Distinct())
            {
                if (string.IsNullOrWhiteSpace(allergy)) continue;
                if (allergyStats.ContainsKey(allergy))
                    allergyStats[allergy]++;
                else
                    allergyStats[allergy] = 1;
            }
        }

        var analyticsDto = new DecAllergAnalyticsDTO
        {
            DeceaseStatistics = deceaseStats,
            AllergyStatistics = allergyStats
        };

        return new List<DecAllergAnalyticsDTO> { analyticsDto };
    }
}
