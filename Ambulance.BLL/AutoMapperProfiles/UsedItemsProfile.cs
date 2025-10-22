using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Entities;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class UsedItemProfile : Profile
{
    public UsedItemProfile()
    {
        CreateMap<UsedItem, UsedItemModel>().ReverseMap();
    }
}
