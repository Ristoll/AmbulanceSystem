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
          .ForMember(dest => dest.Role,
              opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : "Unknown"))
          .ForMember(dest => dest.JwtToken, opt => opt.Ignore()); // токен згенеруємо окремо

        CreateMap<Person, PersonExtModel>();

        CreateMap<PersonCreateModel, Person>()
             .ForMember(dest => dest.Role,
                opt => opt.Ignore()) // тепер не Enum, тому Role встановлюємо вручну після збереження
            .ForMember(dest => dest.Gender,
                  opt => opt.MapFrom(src => ParseUserGender(src.Gender))) // конвертація гендеру-рядка в enum
             .ForMember(dest => dest.PasswordHash,
                  opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)));
    }

    // тимачасове рішення, далі винести в окремий клас конвертерів звідси та з контексту
    private static Gender ParseUserGender(string? v)
        => !string.IsNullOrEmpty(v) && Enum.TryParse<Gender>(v, out var result) ? result : Gender.Other;
}
