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

        // Для маппінгу з CallModel -> Call
        CreateMap<CallModel, Call>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                src.Latitude.HasValue && src.Longitude.HasValue
                    ? new Point(src.Longitude.Value, src.Latitude.Value) { SRID = 4326 }
                    : null
            ));
    }
}
