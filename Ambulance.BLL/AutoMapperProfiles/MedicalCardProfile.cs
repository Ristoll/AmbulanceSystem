using Ambulance.Core.Entities;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class MedicalCardProfile : Profile
{
    public MedicalCardProfile()
    {
        CreateMap<MedicalCard, MedicalCardDto>()
           .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.PersonId))
           .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.PatientAllergies.Select(pa => pa.Allergy.Name).ToList()))
           .ForMember(dest => dest.ChronicDiseases, opt => opt.MapFrom(src => src.PatientChronicDeceases.Select(pcd => pcd.ChronicDecease.Name).ToList()))
           .ForMember(dest => dest.MedicalRecords, opt => opt.MapFrom(src => src.MedicalRecords));

        CreateMap<MedicalCardDto, MedicalCard>()
          .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PatientId))
          .ForMember(dest => dest.PatientAllergies, opt => opt.Ignore()) // окремо
          .ForMember(dest => dest.PatientChronicDeceases, opt => opt.Ignore()) //  окремо
          .ForMember(dest => dest.MedicalRecords, opt => opt.Ignore()) // окремо або використовувати профіль
          .ForMember(dest => dest.Person, opt => opt.Ignore());
    }
}
