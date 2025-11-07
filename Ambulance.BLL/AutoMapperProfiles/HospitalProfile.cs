using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class HospitalProfile : Profile
{
    public HospitalProfile()
    {
        CreateMap<Hospital, HospitalDto>().ReverseMap();
    }
}
