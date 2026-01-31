using Ambulance.DTO;
using AmbulanceSystem.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.BLL.Commands.CallCommands;

internal class LoadHospitalsCommand : AbstrCommandWithDA<List<HospitalDto>>
{
    public override string Name => "Отримати всі лікарні";

    public LoadHospitalsCommand(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
    }

    public override List<HospitalDto> Execute()
    {
        var hospitals = dAPoint.HospitalRepository.GetQueryable()
            .Include(h => h.Brigades)
            .Where(x => x.Brigades.Any())
            .ToList();

        if (!hospitals.Any())
            return new List<HospitalDto>();

        return mapper.Map<List<HospitalDto>>(hospitals);
    }
}
