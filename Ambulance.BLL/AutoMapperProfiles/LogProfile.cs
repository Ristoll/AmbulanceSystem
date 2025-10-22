using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class LogProfile : Profile
{
    public LogProfile()
    {
        CreateMap<Log, LogModel>().ReverseMap();
    }
}
