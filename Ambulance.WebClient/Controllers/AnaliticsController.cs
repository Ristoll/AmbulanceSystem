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
            var analiticsData = manager.GetBrigadeResourceAnalytics(from, to);
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
            var analiticsData = manager.GetCallAnalytics(from, to);
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
            var analiticsData = manager.GetDeceaseAnalytics();
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
            var analiticsData = manager.GetFullAnalytics(from, to);
            return Ok(analiticsData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
