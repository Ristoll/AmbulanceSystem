using Ambulance.BLL.Models;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, AuthResponseModel>()
          .ForMember(dest => dest.UserRole,
              opt => opt.MapFrom(src => src.UserRole != null ? src.UserRole.Name : "Unknown"))
          .ForMember(dest => dest.JwtToken, opt => opt.Ignore()); // токен згенеруємо окремо

        CreateMap<Person, PersonExtModel>();

        CreateMap<PersonCreateModel, Person>()
             .ForMember(dest => dest.UserRole,
                opt => opt.Ignore()) // тепер не Enum, тому Role встановлюємо вручну після збереження
            .ForMember(dest => dest.Gender,
                  opt => opt.MapFrom(src => EnumConverters.ParseUserGender(src.Gender))) // конвертація гендеру-рядка в enum
             .ForMember(dest => dest.PasswordHash,
                  opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)));
    }
}
