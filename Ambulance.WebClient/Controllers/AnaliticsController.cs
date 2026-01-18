using Ambulance.BLL.Commands.AnaliticsCommands;
using AutoMapper;
namespace Ambulance.WebAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "Admin, Dispatcher")]
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
    [Route("brigade-resources")]
    public IActionResult GetResourceAnalytics()
    {
        try
        {
            var analiticsData = manager.GetResourcesAnalytics();
            return Ok(analiticsData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("calls")]
    public IActionResult GetCallAnalitics()
    {
        try
        {
            var analiticsData = manager.GetCallAnalytics();
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
    [Route("allergies")]
    public IActionResult GetAllergyAnalytics()
    {
        try
        {
            var analiticsData = manager.GetAllergyAnalytics();
            return Ok(analiticsData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
