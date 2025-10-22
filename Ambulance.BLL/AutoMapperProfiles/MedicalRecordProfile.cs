using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class MedicalRecordProfile : Profile
{
    public MedicalRecordProfile()
    {
        CreateMap<MedicalRecord, MedicalRecordModel>().ReverseMap();
    }
}
