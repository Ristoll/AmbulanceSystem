using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class MedicalRecordProfile : Profile
{
    public MedicalRecordProfile()
    {
        CreateMap<MedicalRecord, MedicalRecordDto>().ReverseMap();
    }
}
