using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class MedicalCardProfile : Profile
{
    public MedicalCardProfile()
    {
        CreateMap<MedicalCard, MedicalCardDto>().ReverseMap();
    }
}
