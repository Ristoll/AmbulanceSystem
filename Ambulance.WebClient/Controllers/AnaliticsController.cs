using Ambulance.BLL.Commands.AnaliticsCommands;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace Ambulance.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnaliticsController : Controller
{
    private readonly AnaliticsCommandManager manager;
    private readonly IMapper mapper;
    public AnaliticsController(AnaliticsCommandManager manager, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(mapper);
        this.manager = manager;
        this.mapper = mapper;
    }

    [HttpGet]
    [Route("brigade-recources")]
    public IActionResult GetBrigateRecouceAnalitics(DateTime from, DateTime to)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            var analiticsData = manager.GetBrigadeResourceAnalytics(from, to, actionOwnerId);
            return Ok(analiticsData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("calls")]
    public IActionResult GetCallAnalitics(DateTime from, DateTime to)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            var analiticsData = manager.GetCallAnalytics(from, to, actionOwnerId);
            return Ok(analiticsData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("deceases")]
    public IActionResult GetDeceaseAnalitics()
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            var analiticsData = manager.GetDeceaseAnalytics(actionOwnerId);
            return Ok(analiticsData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("full")]
    public IActionResult GetFullAnalitics(DateTime from, DateTime to)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            var analiticsData = manager.GetFullAnalytics(from, to, actionOwnerId);
            return Ok(analiticsData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
