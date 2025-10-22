using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class DispatcherProfile : Profile
{
    public DispatcherProfile()
    {
        CreateMap<Dispatcher, DispatcherModel>().ReverseMap();
    }
}
