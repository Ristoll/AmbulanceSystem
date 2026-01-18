using Ambulance.Core.Entities;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class MedicalRecordProfile : Profile
{
    public MedicalRecordProfile()
    {
        CreateMap<MedicalRecord, MedicalRecordDto>()
           .ForMember(dest => dest.BrigadeMemberName, opt => opt.MapFrom(src =>
               src.BrigadeMember != null ?  $"{src.BrigadeMember.Person.Name} {src.BrigadeMember.Person.Surname}" : null))
           .ForMember(dest => dest.MedicalCardPatientName, opt => opt.MapFrom(src =>
               src.MedicalCard != null && src.MedicalCard.Person != null ? $"{src.MedicalCard.Person.Name} {src.MedicalCard.Person.Surname}" : null))
           .ReverseMap()
           .ForMember(dest => dest.BrigadeMember, opt => opt.Ignore())
           .ForMember(dest => dest.MedicalCard, opt => opt.Ignore()); // щоб не було циклу при мапінгу картки
    }
}
