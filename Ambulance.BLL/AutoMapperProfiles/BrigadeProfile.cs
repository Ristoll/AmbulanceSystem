using Ambulance.Core.Entities;
using Ambulance.Core.Entities.StandartEnums;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeProfile : Profile
{
    public BrigadeProfile()
    {
        CreateMap<Brigade, BrigadeDto>()
            .ForMember(dest => dest.BrigadeStateName,
                opt => opt.MapFrom(src => EnumConverters.GetBrigadeStateUkrainian(src.BrigadeState))) // Українська назва
            .ForMember(dest => dest.BrigadeStateCode,
                opt => opt.MapFrom(src => src.BrigadeState.ToEnumString(BrigadeState.Unknown))) // Англійський код
            .ForMember(dest => dest.BrigadeTypeName,
                opt => opt.MapFrom(src => src.BrigadeType));
    }
}