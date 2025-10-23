using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using NetTopologySuite.Geometries;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeProfile : Profile
{
    public BrigadeProfile()
    {
        CreateMap<Brigade, BrigadeModel>().ReverseMap();
        // Для маппінгу з CallModel -> Call
        CreateMap<BrigadeModel, Brigade>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src =>
                src.Latitude.HasValue && src.Longitude.HasValue
                    ? new Point(src.Longitude.Value, src.Latitude.Value) { SRID = 4326 }
                    : null
            ));
    }
}