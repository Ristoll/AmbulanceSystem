using Ambulance.Core;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using AmbulanceSystem.DTO;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    public class UpdateMedicalRecordCommand : AbstrCommandWithDA<bool>
    {
        private readonly MedicalRecordDto medicalRecordDto;

        public UpdateMedicalRecordCommand(MedicalRecordDto medicalRecordDto, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.medicalRecordDto = medicalRecordDto;
        }

        public override string Name => "Оновлення медичного запису";

        public override bool Execute()
        {
            var existingRecord = dAPoint.MedicalRecordRepository
                .FirstOrDefault(mc => mc.MedicalRecordId == medicalRecordDto.MedicalRecordId);
            
            if (existingRecord == null)
                throw new InvalidOperationException($"Медичний запис для пацієнта не знайдено");

            mapper.Map(medicalRecordDto, existingRecord);

            dAPoint.MedicalRecordRepository.Update(existingRecord);
            dAPoint.Save();

            return true;
        }
    }
}
