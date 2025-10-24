using Ambulance.BLL.Models;
using Ambulance.DTO;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace Ambulance.WebClient.AutoMapperProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonRequest, PersonCreateModel>();
        CreateMap<Person, PersonExtDTO>();
    }
}
