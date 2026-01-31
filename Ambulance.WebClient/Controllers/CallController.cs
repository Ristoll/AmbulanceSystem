using Ambulance.BLL.Commands;
using Ambulance.BLL.Commands.CallCommands;
using Ambulance.DTO;
using Ambulance.DTO.PersonModels;
using Ambulance.WebAPI.Hubs;
using AmbulanceSystem.DAL;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Ambulance.WebAPI.Controllers;

[ApiController]
[Authorize(Roles = "Admin, Dispatcher")]
[Route("api/[controller]")]
public class CallController : Controller
{
    private readonly IHubContext<NotificationHub> hubContext;
    private readonly CallCommandManager manager;
    private readonly IMapper mapper;
    public CallController(CallCommandManager manager, IMapper mapper, IHubContext<NotificationHub> hubContext)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(mapper);
        this.manager = manager;
        this.mapper = mapper;
        this.hubContext = hubContext;
    }

    //[HttpDelete("delete-call/{callId}")]
    //public IActionResult DeleteCall(int callId)
    //{
    //    try
    //    {
    //        bool result = manager.DeleteCallCommand(callId);
    //        return result ? Ok() : BadRequest("Не вдалося видалити виклик");
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    //[HttpPost("create-and-fill-call")]
    //public async Task<IActionResult> CreateAndFillCall([FromBody] FillCallFullRequest request)
    //{
    //    try
    //    {
    //        var callDto = request.Call;
    //        var patientData = request.Person;

    //        callDto.DispatcherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    //        int callId = manager.CreateAndFillCallCommand(callDto, patientData);

    //        // повідомлення бригадам
    //        string notificationText = "Отримано новий виклик! Пішли працювати!!!";

    //        var brigadeMembers = request.Call.AssignedBrigades?
    //            .Where(b => b.Members != null)
    //            .SelectMany(b => b.Members!)
    //            .ToList();

    //        if (brigadeMembers != null && brigadeMembers.Any())
    //        {
    //            foreach (var brigadeMember in brigadeMembers)
    //            {
    //                if (brigadeMember?.BrigadeMemberId != null)
    //                {
    //                    await hubContext.Clients.User(brigadeMember.BrigadeMemberId.ToString())
    //                        .SendAsync("ReceiveNotification", notificationText);
    //                }
    //            }
    //        }

    //        return Ok(callId);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    [HttpGet("load-call")]
    public IActionResult LoadCall([FromQuery] int callId)
    {
        try
        {
            var call = manager.LoadCallCommand(callId);
            return Ok(call);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("load-calls")]
    public IActionResult LoadCalls()
    {
        try
        {
            var calls = manager.LoadCallsCommand();
            return Ok(calls);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //[HttpGet("load-hospitals")]
    //public IActionResult LoadHospitalsCommand()
    //{
    //    try
    //    {
    //        var hospitals = manager.LoadHospitalsCommand();
    //        return Ok(hospitals);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    [HttpGet("search-person/{text?}")]
    public IActionResult SearchPatient(string? text)
    {
        try
        {
            var persons = manager.SearchPatientCommand(text);
            return Ok(persons);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
