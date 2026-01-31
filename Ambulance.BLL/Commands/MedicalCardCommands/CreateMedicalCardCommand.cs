using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities;

namespace Ambulance.BLL.Commands.MedicalCardCommands;

public class CreateMedicalCardCommand : AbstrCommandWithDA<bool>
{
    private readonly MedicalCardDto medicalCardDto;

    public CreateMedicalCardCommand(MedicalCardDto medicalCardDto, IUnitOfWork operateUnitOfWork, IMapper mapper)
        : base(operateUnitOfWork, mapper)
    {
        if (medicalCardDto == null)
            throw new ArgumentNullException("DTO медичної картки null");

        this.medicalCardDto = medicalCardDto;
    }

    public override string Name => "Створення медичної картки";

    public override bool Execute()
    {
        var medicalCard = mapper.Map<MedicalCard>(medicalCardDto);

        dAPoint.MedicalCardRepository.Add(medicalCard);
        dAPoint.Save();

        return true;
    }
}

