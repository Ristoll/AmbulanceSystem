using Ambulance.BLL.Models;
using Ambulance.DTO;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace Ambulance.WebClient.AutoMapperProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonRequest, PersonCreateModel>();
        CreateMap<PersonExtModel, PersonExtDTO>();
    }
}
