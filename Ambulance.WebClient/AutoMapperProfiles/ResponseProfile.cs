using Ambulance.BLL.Models;
using Ambulance.DTO;
using AutoMapper;

namespace Ambulance.WebClient.AutoMapperProfiles;

public class ResponseProfile : Profile
{
    public ResponseProfile()
    {
        CreateMap<AuthResponseModel, AuthResponseDto>();
    }
}
