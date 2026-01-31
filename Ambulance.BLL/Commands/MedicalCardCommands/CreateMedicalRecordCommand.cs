using AutoMapper;
using AmbulanceSystem.DTO;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
    internal class CreateMedicalRecordCommand : AbstrCommandWithDA<bool>
    {
        private readonly MedicalRecordDto medicalRecordDto;

        public CreateMedicalRecordCommand(MedicalRecordDto medicalRecordDto, IUnitOfWork operateUnitOfWork, IMapper mapper) 
            : base(operateUnitOfWork, mapper)
        {
            ArgumentNullException.ThrowIfNull(medicalRecordDto, "DTO медичного запису null");

            this.medicalRecordDto = medicalRecordDto;
        }

        public override string Name => "Створення медичного запису";

        public override bool Execute()
        {
            var medicalRecordEntity = mapper.Map<MedicalRecord>(medicalRecordDto);
            dAPoint.MedicalRecordRepository.Add(medicalRecordEntity);
            dAPoint.Save();

            return true;
        }
    }
}
