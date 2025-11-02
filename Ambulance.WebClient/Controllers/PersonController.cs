using Ambulance.BLL.Commands;
using Ambulance.BLL.Models;
using Ambulance.DTO;
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
    public IActionResult CreatePerson([FromBody] CreatePersonRequest request)
    {
        try
        {
            // отримуємо ID користувача, який виконав запит із JWT токена
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);

            var model = mapper.Map<PersonCreateModel>(request);
            bool result = manager.CreatePerson(model, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося створити користувача");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create-patient")]
    [Authorize(Roles = "Dispatcher")]
    public IActionResult CreatePatient([FromBody] CreatePersonRequest request)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);

            var model = mapper.Map<PersonCreateModel>(request);
            model.Role = "Patient"; // жорстко встановлюємо роль
            bool result = manager.CreatePerson(model, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося створити пацієнта");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("authetificate")]
    public ActionResult<AuthResponseDto> Authetificate([FromBody] LoginRequest request)
    {
        try
        {
            var response = manager.AuthPerson(request.Login, request.Password); // повертає базову інформацію про користувача з токеном

            var responseDto = mapper.Map<AuthResponseDto>(response);
            return responseDto;
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
