using Ambulance.Core.Entities;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace Ambulance.BLL.AutoMapperProfiles;

public class UserRoleProfile : Profile
{
    public UserRoleProfile()
    {
        CreateMap<UserRole, UserRoleViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserRoleId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
