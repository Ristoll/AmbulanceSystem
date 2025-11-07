using Ambulance.BLL.Commands;
using Ambulance.BLL.Commands.MedicalCardCommands;
using Ambulance.DTO;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Ambulance.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MedicalCardController : Controller
{
    private readonly MedicalCardCommandManager manager;
    private readonly IMapper mapper;
    public MedicalCardController(MedicalCardCommandManager manager, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(mapper);
        this.manager = manager;
        this.mapper = mapper;
    }

    [HttpPost("create-medical-card")]
    public IActionResult CreateMedicalCard([FromBody] MedicalCardDto medicalCardDto)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.CreateMedicalCardCommand(medicalCardDto, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося створити медичну карту");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("update-medical-card")]
    public IActionResult UpdateMedicalCard([FromBody] MedicalCardDto medicalCardDto)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.UpdateMedicalCardCommand(medicalCardDto, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося оновити медичну карту");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search-medical-card/{personId}")]
    public IActionResult SearchMedicalCard(int personId)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.SearchMeducalCardCommand(personId, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося знайти медичну карту");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create-medical-record")]
    public IActionResult CreateMedicalRecord([FromBody] MedicalRecordDto medicalRecordDto)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.CreateMedicalRecordCommand(medicalRecordDto, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося створити медичний запис");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
