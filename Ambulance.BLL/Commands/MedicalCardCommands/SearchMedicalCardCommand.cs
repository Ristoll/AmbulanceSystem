using AutoMapper;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.MedicalCardCommands;

public class SearchMedicalCardCommand : AbstrCommandWithDA<bool>
{
    private readonly int personId;

    public SearchMedicalCardCommand(int personId, IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
        this.personId = personId;
    }

    public override string Name => "Пошук медичної картки";

    public override bool Execute()
    {
        var medicalCard = dAPoint.MedicalCardRepository
            .FirstOrDefault(mc => mc.PatientId == personId);

        ArgumentNullException.ThrowIfNull(medicalCard, $"{Name}: Медична картка не знайдена для особи з ID {personId}");

        return true;
    }
}
