using AutoMapper;
using Ambulance.DTO;
using Ambulance.Core.Entities;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class HospitalProfile : Profile
{
    public HospitalProfile()
    {
        CreateMap<Hospital, HospitalDto>(); // бригади самі знайдуться
    }
}
