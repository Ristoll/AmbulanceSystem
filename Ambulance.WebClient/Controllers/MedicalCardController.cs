using Ambulance.BLL.Commands;
using Ambulance.BLL.Commands.MedicalCardCommands;
using Ambulance.DTO;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.DTO;
using AutoMapper;
namespace Ambulance.WebAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "Admin, Dispatcher, BrigadeMan")]
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
            bool result = manager.CreateMedicalCardCommand(medicalCardDto);
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
            bool result = manager.UpdateMedicalCardCommand(medicalCardDto);
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
            bool result = manager.SearchMeducalCardCommand(personId);
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
            bool result = manager.CreateMedicalRecordCommand(medicalRecordDto);
            return result ? Ok() : BadRequest("Не вдалося створити медичний запис");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("update-medical-record")]
    public IActionResult UpdateMedicalRecord([FromBody] MedicalRecordDto medicalRecordDto)
    {
        try
        {
            bool result = manager.UpdateMedicalRecordCommand(medicalRecordDto);
            return result ? Ok() : BadRequest("Не вдалося оновити медичний запис");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("load-medical-records/{medicalCardId}")]
    public IActionResult LoadMedicalRecords(int medicalCardId)
    {
        try
        {
            var result = manager.LoadMedicalRecordsCommand(medicalCardId);
            return result != null ? Ok(result) : BadRequest("Не вдалося завантажити медичні записи");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("load-medical-record/{medicalRecordId}")]
    public IActionResult LoadMedicalRecord(int medicalRecordId)
    {
        try
        {
            var result = manager.LoadMedicalRecordCommand(medicalRecordId);
            return result != null ? Ok(result) : BadRequest("Не вдалося завантажити медичний запис за ідентифікатором");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("load-all-medical-cards")]
    public IActionResult LoadAllMedicalCards()
    {
        try
        {
            var result = manager.LoadAllMedicalCardsCommand();
            return result != null ? Ok(result) : BadRequest("Не вдалося завантажити медичні карти");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("load-medical-card/{medicalCardId}")]
    public IActionResult LoadMedicalCard(int medicalCardId)
    {
        try
        {
            var result = manager.LoadMedicalCardCommand(medicalCardId);
            return result != null ? Ok(result) : BadRequest("Не вдалося завантажити медичну карту за ідентифікатором");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
