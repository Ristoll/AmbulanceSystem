using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientModel>().ReverseMap();
    }
}
