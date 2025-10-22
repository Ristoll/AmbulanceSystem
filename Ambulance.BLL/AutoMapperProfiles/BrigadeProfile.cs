using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeProfile : Profile
{
    public BrigadeProfile()
    {
        CreateMap<Brigade, BrigadeModel>().ReverseMap();
    }
}