using Ambulance.BLL.Commands;
using Ambulance.Core.Entities.StandartEnums;
using Ambulance.DTO;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.DAL;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ambulance.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : Controller
{
    [HttpGet("ping")]
    public IActionResult Ping() => Ok("API працює");

    private readonly PersonIdentityCommandManager manager;
    private readonly IMapper mapper;

    public PersonController(PersonIdentityCommandManager manager, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(mapper);

        this.manager = manager;
        this.mapper = mapper;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")] // одразу перевіряємо, чи є користувач адміністратором
    public IActionResult CreatePerson([FromBody] PersonCreateRequest request)
    {
        try
        {
            var result = manager.CreatePerson(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create-patient")]
    [Authorize(Roles = "Admin")]
    public IActionResult CreatePatient([FromBody] PersonCreateRequest request)
    {
        try
        {
            request.Role = UserRole.Patient.ToString(); // жорстко встановлюємо роль
            var result = manager.CreatePerson(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("authetificate")]
    public ActionResult<AuthResponse> Authetificate([FromBody] LoginRequest request)
    {
        try
        {
            var response = manager.AuthPerson(request); // повертає базову інформацію про користувача з токеном

            return response;
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("change-password")]
    [Authorize]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            bool result = manager.ChangePassword(request, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося змінити пароль");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("admin-reset-password/{targetPersonId}")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminResetPassword(int targetPersonId, [FromBody] string newPassword)
    {
        try
        {
            bool result = manager.AdminResetPassword(newPassword, targetPersonId);
            return result ? Ok() : BadRequest("Не вдалося скинути пароль користувача");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("profile/{personId}")]
    [Authorize]
    public ActionResult<PersonProfileDTO> GetPersonProfile(int personId)
    {
        try
        {
            var profile = manager.GetPersonProfile(personId);
            return profile != null ? Ok(profile) : NotFound("Користувача не знайдено");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("patientData/{personId}")]
    [Authorize]
    public ActionResult<PatientDto> GetPatientData(int personId)
    {
        try
        {
            var profile = manager.LoadPatientData(personId);
            return profile != null ? Ok(profile) : NotFound("Користувача не знайдено");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
 
    [HttpPut("update")]
    [Authorize]
    public IActionResult UpdatePerson([FromBody] PersonUpdateDTO updateModel)
    {
        try
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            updateModel.PersonId = currentUserId; // гарантуємо, що користувач оновлює лише свої дані
            updateModel.Role =null; // забороняємо змінювати роль через цей метод
            bool result = manager.UpdatePerson(updateModel);
            return result ? Ok() : BadRequest("Не вдалося оновити дані користувача");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("admin-update")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminUpdatePerson([FromBody] PersonUpdateDTO updateModel)
    {
        try
        {
            bool result = manager.UpdatePerson(updateModel);
            return result ? Ok() : BadRequest("Не вдалося оновити дані користувача");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete/{deletePersonId}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeletePerson(int deletePersonId)
    {
        try
        {
            bool result = manager.DeletePerson(deletePersonId);
            return result ? Ok() : BadRequest("Не вдалося видалити користувача");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("load-persons")]
    [Authorize]
    public ActionResult<List<PersonExtDTO>> LoadPersons()
    {
        try
        {
            var list = manager.LoadPersons();
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("roles")]
    [Authorize]
    public ActionResult<List<string>> LoadPersonRoles()
    {
        try
        {
            var roles = manager.LoadPersonRoles();
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}