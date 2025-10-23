using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class ActionLogProfile : Profile
{
    public ActionLogProfile()
    {
        CreateMap<ActionLog, ActionLogModel>().ReverseMap();
    }
}
