using Ambulance.DTO;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class ActionLogProfile : Profile
{
    public ActionLogProfile()
    {
        CreateMap<ActionLog, ActionLogDTO>().ReverseMap();
    }
}
