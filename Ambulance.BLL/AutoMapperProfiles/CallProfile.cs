using Ambulance.Core.Entities;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.DTO;
using AutoMapper;

namespace AmbulanceSystem.BLL.AutoMapperProfiles;

public class CallProfile : Profile
{
    public CallProfile()
    {
        //CreateMap<Call, CallDto>()
        //    .ForMember(d => d.AssignedBrigades,
        //        o => o.MapFrom(s => s.Brigades))
        //    .ForMember(d => d.DispatcherIndentificator,
        //        o => o.MapFrom(s => $"{s.Dispatcher.Surname} #{s.Dispatcher.PersonId}"))
        //    .ForMember(d => d.PatientFullName,
        //        o => o.MapFrom(s => $"{s.Patient.Surname} {s.Patient.Name} {s.Patient.MiddleName}"))
        //    .ForMember(d => d.HospitalName,
        //        o => o.MapFrom(s => s.Hospital != null ? s.Hospital.Name : null))
        //    .ForMember(d => d.EstimatedArrival,
        //        o => o.Ignore()); // рахується окремо у сервісі


        //CreateMap<CallDto, Call>()
        //    .ForMember(d => d.CallId, o => o.Ignore())
        //    .ForMember(d => d.Brigades, o => o.Ignore()) // їх окремо оновлюють
        //    .ForMember(d => d.Dispatcher, o => o.Ignore())
        //    .ForMember(d => d.Patient, o => o.Ignore())
        //    .ForMember(d => d.Hospital, o => o.Ignore());
    }
}
