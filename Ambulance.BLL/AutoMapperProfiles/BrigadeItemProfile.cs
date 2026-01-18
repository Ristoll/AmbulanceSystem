using Ambulance.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeItemProfile : Profile
{
    public BrigadeItemProfile()
    {
        CreateMap<BrigadeItem, BrigadeItemDto>().ReverseMap();
    }
}

