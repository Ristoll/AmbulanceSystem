using Ambulance.BLL.Commands;
using Ambulance.BLL.Models.PersonModels;
using Ambulance.DTO;
using Ambulance.DTO.PersonModels;
using AmbulanceSystem.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambulance.WebClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : Controller
{
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
            // отримуємо ID користувача, який виконав запит із JWT токена
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);

            bool result = manager.CreatePerson(request, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося створити користувача");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create-patient")]
    [Authorize(Roles = "Dispatcher")]
    public IActionResult CreatePatient([FromBody] PersonCreateRequest request)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);

            request.Role = "Patient"; // жорстко встановлюємо роль
            bool result = manager.CreatePerson(request, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося створити пацієнта");
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
}
