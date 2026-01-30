using Ambulance.Core;
using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.MedicalCardCommands
{
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
                .GetAll().Where(mr => mr.CardId == medicalCardId);
            return mapper.Map<List<MedicalRecordDto>>(medicalRecords);
        }

    }
}