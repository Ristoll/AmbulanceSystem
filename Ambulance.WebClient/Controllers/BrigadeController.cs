using Ambulance.BLL.Commands.BigadeCommands;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ambulance.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BrigadeController : Controller
{
    private readonly BrigadeCommandManager manager;
    private readonly IMapper mapper;
    public BrigadeController(BrigadeCommandManager manager, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(mapper);
        this.manager = manager;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllBrigades()
    {
        try
        {
            var brigades = manager.LoadAllBrigades();
            return Ok(brigades);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("{brigadeId:int}")]
    public IActionResult GetBrigadeById(int brigadeId)
    {
        try
        {
            var brigade = manager.LoadBrigade(brigadeId);
            return Ok(brigade);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("state/{brigadeStateId:int}")]
    public IActionResult GetBrigadesState(int brigadeStateId)
    {
        try
        {
            var brigadesState = manager.LoadBrigadesByState(brigadeStateId);
            return Ok(brigadesState);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("update-brigade-state")]
    public IActionResult UpdateBrigadeState([FromBody] BrigadeDto brigadeDto, int brigadeStateId)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.ChangeBrigadeState(brigadeStateId, brigadeDto);
            return result ? Ok() : BadRequest("Не вдалося оновити стан бригади");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("update-brigade")]
    public IActionResult UpdateBrigade([FromBody] BrigadeDto brigadeDto)
    {
        try
        {
            var updatedBrigade = manager.UpdateBrigade(brigadeDto);
            return Ok(updatedBrigade);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
