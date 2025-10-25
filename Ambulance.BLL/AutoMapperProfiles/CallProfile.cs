using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using NetTopologySuite.Geometries;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class CallProfile : Profile
{
    public CallProfile()
    {
        CreateMap<Call, CallModel>().ReverseMap();
    }
}
