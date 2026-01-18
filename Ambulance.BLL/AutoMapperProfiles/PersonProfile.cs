using Ambulance.Core.Entities;
using Ambulance.DTO.PersonModels;
using Ambulance.ExternalServices;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonExtDTO>()
            .ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => EnumConverters.ParseUserRole(src.UserRole)))
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => EnumConverters.ParseUserGender(src.Gender)));

        CreateMap<Person, PersonProfileDTO>()
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => EnumConverters.ParseUserGender(src.Gender)));

        CreateMap<Person, PatientDto>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId)); // для безпечності, бо може бути ігноровано

        CreateMap<Person, AuthResponse>()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Login) ? src.Login : src.PhoneNumber))
            .ForMember(dest => dest.UserRole,
                opt => opt.MapFrom(src => EnumConverters.ParseUserRole(src.UserRole)))
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId)) // для безпечності, бо може бути ігноровано
            .ForMember(dest => dest.JwtToken, opt => opt.Ignore());

        CreateMap<PersonCreateRequest, Person>()
            .ForMember(dest => dest.UserRole,
                opt => opt.MapFrom(src => EnumConverters.ParseUserRole(src.Role)))
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => EnumConverters.ParseUserGender(src.Gender)))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)));
    }
}
