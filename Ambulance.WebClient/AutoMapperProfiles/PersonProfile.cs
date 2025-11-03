using Ambulance.BLL.Models.PersonModels;
using Ambulance.DTO;
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
