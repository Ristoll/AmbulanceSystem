using Ambulance.Core.Entities;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeMemberProfile : Profile
{
    public BrigadeMemberProfile()
    {
        CreateMap<BrigadeMember, BrigadeMemberDto>()
            .ForMember(dest => dest.PersonFullName,
                opt => opt.MapFrom(src => $"{src.Person.Surname} {src.Person.MiddleName} {src.Person.Surname}"));
    }
}
