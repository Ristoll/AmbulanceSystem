using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using NetTopologySuite.Geometries;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class HospitalProfile : Profile
{
    public HospitalProfile()
    {
        CreateMap<Hospital, HospitalModel>().ReverseMap();
        // Для маппінгу з HospitalModel -> Hospital
        CreateMap<HospitalModel, Hospital>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src =>
                src.Latitude.HasValue && src.Longitude.HasValue
                    ? new Point(src.Longitude.Value, src.Latitude.Value) { SRID = 4326 }
                    : null
            ));
    }
}
