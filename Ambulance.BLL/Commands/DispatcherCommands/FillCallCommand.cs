using Ambulance.BLL.Commands;
using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core.Data;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambulance.BLL.Commands.CallsCommands;
public class FillCallCommand : AbstrCommandWithDA<bool>
{
    private readonly CallModel callModel;

    public override string Name => "Заповнення виклику";
    public FillCallCommand(CallModel callModel, IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
        this.callModel = callModel;
    }

    public override bool Execute()
    {
        var call = dAPoint.CallRepository.GetById(callModel.CallId);
        if (call == null)
        {
            LogAction($"{Name}: виклик з ID {callModel.CallId} не знайдено");
            return false;
        }

        call.EndCallTime = DateTime.Now;
        call.Notes = callModel.Notes;
        call.DispatcherId = callModel.DispatcherId;
        call.PatientId = callModel.PatientId;
        call.HospitalId = callModel.HospitalId;
        call.UrgencyType = callModel.UrgencyType;

        if (callModel.Latitude.HasValue && callModel.Longitude.HasValue)
        {
            call.Address = new Point(callModel.Longitude.Value, callModel.Latitude.Value)
            {
                SRID = 4326 // стандарт WGS84
            };
        }
        else
        {
            call.Address = null;
        }

        dAPoint.CallRepository.Update(call);
        dAPoint.Save();

        LogAction($"{Name} № {call.CallId}");
        return true;
    }
}