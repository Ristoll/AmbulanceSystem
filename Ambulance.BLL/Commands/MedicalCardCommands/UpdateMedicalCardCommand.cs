using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.MedicalCardCommands;

public class UpdateMedicalCardCommand : AbstrCommandWithDA<bool>
{
    private readonly MedicalCardDto medicalCardDto;

    public UpdateMedicalCardCommand(MedicalCardDto medicalCardDto, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        ArgumentNullException.ThrowIfNull(medicalCardDto, "DTO медичної картки null");

        this.medicalCardDto = medicalCardDto;
    }

    public override string Name => "Оновлення медичної картки";

    public override bool Execute()
    {
        var existingCard= dAPoint.MedicalCardRepository
            .FirstOrDefault(p => p.CardId == medicalCardDto.PatientId);

        ArgumentNullException.ThrowIfNull(existingCard, $"Медичну картку для пацієнта {medicalCardDto.PatientId} не знайдено");

        mapper.Map(medicalCardDto, existingCard);

        dAPoint.MedicalCardRepository.Update(existingCard);
        dAPoint.Save();

        return true;
    }
}
