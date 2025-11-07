using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class CallProfile : Profile
{
    public CallProfile()
    {
        CreateMap<Call, CallDto>().ReverseMap();
    }
}
