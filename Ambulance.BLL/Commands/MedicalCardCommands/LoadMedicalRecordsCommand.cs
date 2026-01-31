using AutoMapper;
using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;

namespace Ambulance.BLL.Commands.MedicalCardCommands;

public class LoadMedicalRecordsCommand : AbstrCommandWithDA<List<MedicalRecordDto>>
{
    private readonly int medicalCardId;

    public LoadMedicalRecordsCommand(int medicalCardId, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.medicalCardId = medicalCardId;
    }

    public override string Name => "Завантаження медичних записів";

    public override List<MedicalRecordDto> Execute()
    {
        var medicalRecords = dAPoint.MedicalRecordRepository
            .GetQueryable().Where(mr => mr.CardId == medicalCardId);

        return mapper.Map<List<MedicalRecordDto>>(medicalRecords);
    }
}