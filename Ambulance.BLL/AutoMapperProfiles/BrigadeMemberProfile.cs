using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeMemberProfile : Profile
{
    public BrigadeMemberProfile()
    {
        CreateMap<BrigadeMember, BrigadeMemberDto>().ReverseMap();
    }
}
