using Ambulance.BLL.Commands.BigadeCommands;
using AmbulanceSystem.DTO;
using AutoMapper;
namespace Ambulance.WebAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "Admin, Dispatcher, BrigadeMan")]
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

    //[HttpGet]
    //public IActionResult GetAllBrigades()
    //{
    //    try
    //    {
    //        var brigades = manager.LoadAllBrigades();
    //        return Ok(brigades);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
    //[HttpGet]
    //[Route("{brigadeId:int}")]
    //public IActionResult GetBrigadeById(int brigadeId)
    //{
    //    try
    //    {
    //        var brigade = manager.LoadBrigade(brigadeId);
    //        return Ok(brigade);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
    [HttpGet]
    [Route("state/{stateName}")]
    public IActionResult GetBrigadesState(string stateName)
    {
        try
        {
            var brigadesState = manager.LoadBrigadesByState(stateName);
            return Ok(brigadesState);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("update-brigade-state/{brigadeId}")]
    public IActionResult UpdateBrigadeState(int brigadeId, [FromBody] string stateName)
    {
        try
        {
            bool result = manager.ChangeBrigadeState(stateName, brigadeId);
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
    //[HttpGet("brigade-type/{brigadeId}")]
    //public IActionResult GetBrigadeType(int brigadeId)
    //{
    //    try
    //    {
    //        var brigadeType = manager.LoadBrigadeType(brigadeId);
    //        return Ok(brigadeType);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
    //[HttpGet("brigade-members/{brigadeId}")]
    //public IActionResult GetBrigadeMembers(int brigadeId)
    //{
    //    try
    //    {
    //        var brigadeMembers = manager.LoadAllBrigadeMembers(brigadeId);
    //        return Ok(brigadeMembers);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
    //[HttpGet("brigade-member/{brigadeMemberId}")]
    //public IActionResult GetBrigadeMember(int brigadeMemberId)
    //{
    //    try
    //    {
    //        var brigadeMember = manager.LoadBrigadeMember(brigadeMemberId);
    //        return Ok(brigadeMember);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
    //[HttpGet("brigade-member-role/{brigadeMemberId}")]
    //public IActionResult GetBrigadeMemberRoleName(int brigadeMemberId)
    //{
    //    try
    //    {
    //        var roleName = manager.LoadBrigadeMemberRoleName(brigadeMemberId);
    //        return Ok(roleName);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
    //[HttpGet("brigade-member-specialization/{brigadeMemberId}")]
    //public IActionResult GetBrigadeMemberSpecializationTypeName(int brigadeMemberId)
    //{
    //    try
    //    {
    //        var specializationTypeName = manager.LoadBrigadeMemberSpecializationTypeName(brigadeMemberId);
    //        return Ok(specializationTypeName);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}
    
}
