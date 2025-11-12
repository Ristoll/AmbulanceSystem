using Ambulance.BLL.Commands;
using Ambulance.BLL.Commands.CallCommands;
using Ambulance.DTO;
using Ambulance.DTO.PersonModels;
using Ambulance.WebAPI.Hubs;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Ambulance.WebAPI.Controllers;

[ApiController]
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

    [HttpPost("create-call")]

    public IActionResult CreateCall([FromBody] CallDto callDto)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.CreateCallCommand(callDto, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося створити виклик");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete-call/{callId}")]
    public IActionResult DeleteCall(int callId)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.DeleteCallCommand(callId, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося видалити виклик");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("fill-call")]
    public async Task<IActionResult> FillCall([FromBody] FillCallRequest request)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);

            bool result = manager.FillCallCommand(
                request.CallDto,
                request.PatientDto,
                request.PersonCreateRequest,
                actionOwnerId
            );

            if (!result)
                return BadRequest("Не вдалося заповнити виклик");

            string notificationText = $"Отримано новий виклик!";

            var brigadeMembers = request.CallDto.AssignedBrigades!.SelectMany(b => b.Members!).ToList();

            foreach (var brigadeMember in brigadeMembers)
            {
                await hubContext.Clients.User(brigadeMember.BrigadeMemberId.ToString())
                    .SendAsync("ReceiveNotification", notificationText);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

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

    [HttpGet("search-person")]
    public IActionResult SearchPerson([FromQuery] PersonProfileDTO personProfileDTO)
    {
        try
        {
            var persons = manager.SearchPersonCommand(personProfileDTO);
            return Ok(persons);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
