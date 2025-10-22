using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class HospitalProfile : Profile
{
    public HospitalProfile()
    {
        CreateMap<Hospital, HospitalModel>().ReverseMap();
    }
}
