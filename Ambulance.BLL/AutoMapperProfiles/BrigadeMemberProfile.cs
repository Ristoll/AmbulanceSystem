using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class BrigadeMemberProfile : Profile
{
    public BrigadeMemberProfile()
    {
        CreateMap<BrigadeMember, BrigadeMemberModel>().ReverseMap();
    }
}
