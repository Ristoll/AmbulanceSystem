using AutoMapper;
using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;

namespace Ambulance.BLL.Commands.MedicalCardCommands;

public class LoadMedicalRecordCommand : AbstrCommandWithDA<MedicalRecordDto>
{
    private readonly int medicalRecordId;

    public LoadMedicalRecordCommand(int medicalRecordId, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.medicalRecordId = medicalRecordId;
    }

    public override string Name => "Завантаження медичного запису";

    public override MedicalRecordDto Execute()
    {
        var medicalRecord = dAPoint.MedicalRecordRepository
            .FirstOrDefault(mr => mr.RecordId == medicalRecordId);

        ArgumentNullException.ThrowIfNull(medicalRecord, $"{Name}: Медичний запис не знайдено з ID {medicalRecordId}");

        return mapper.Map<MedicalRecordDto>(medicalRecord);
    }
}
