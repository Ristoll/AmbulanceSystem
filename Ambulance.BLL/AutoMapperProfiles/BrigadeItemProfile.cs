using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeItemProfile : Profile
{
    public BrigadeItemProfile()
    {
        CreateMap<BrigadeItem, BrigadeItemModel>().ReverseMap();
    }
}

