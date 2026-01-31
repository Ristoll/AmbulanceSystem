using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallCommands;

//internal class LoadHospitalsCommand : AbstrCommandWithDA<List<HospitalDto>>
//{
//    public override string Name => "Отримати всі виклики";

//    public LoadHospitalsCommand(IUnitOfWork unitOfWork, IMapper mapper)
//        : base(unitOfWork, mapper)
//    {
//    }

//    public override List<HospitalDto> Execute()
//    {
//        var hospitals = dAPoint.HospitalRepository.GetQueryable()
//            .Include(h => h.Brigades)
//            .Where(x => x.Brigades.Any())
//            .ToList();

//        if (!hospitals.Any())
//            return new List<HospitalDto>();

//        return mapper.Map<List<HospitalDto>>(hospitals);
//    }
//}
