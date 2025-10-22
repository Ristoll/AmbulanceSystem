using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class MedicalCardProfile : Profile
{
    public MedicalCardProfile()
    {
        CreateMap<MedicalCard, MedicalCardModel>().ReverseMap();
    }
}
