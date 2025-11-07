using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeProfile : Profile
{
    public BrigadeProfile()
    {
        CreateMap<Brigade, BrigadeDto>().ReverseMap();
    }
}