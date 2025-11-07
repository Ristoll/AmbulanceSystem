using Ambulance.DTO.PersonModels;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonExtDTO>();
        CreateMap<Person, PersonProfileDTO>();

        CreateMap<Person, AuthResponse>()
          .ForMember(dest => dest.UserRole,
              opt => opt.MapFrom(src => src.UserRole != null ? src.UserRole.Name : "Unknown")) // тут достатньо саме тільки ролі
          .ForMember(dest => dest.JwtToken, opt => opt.Ignore()); // токен згенеруємо окремо
        
        CreateMap<PersonCreateRequest, Person>()
            .ForMember(dest => dest.UserRole, opt => opt.Ignore())
            .ForMember(dest => dest.Gender,
                  opt => opt.MapFrom(src => EnumConverters.ParseUserGender(src.Gender))) // конвертація гендеру-рядка в enum
             .ForMember(dest => dest.PasswordHash,
                  opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)));
    }
}
