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
    [Authorize(Roles = "Admin")] // лише адміністратори можуть створювати
    public IActionResult CreatePerson([FromBody] CreatePersonRequest request)
    {
        try
        {
            // отримуємо ID користувача, який виконав запит із JWT токена
            var actionOwnerId = int.Parse(User.FindFirst("id")!.Value);

            var model = mapper.Map<PersonCreateModel>(request);
            var result = manager.CreatePerson(model, actionOwnerId);
            return result ? Ok() : BadRequest("Не вдалося створити користувача");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost("authetificate")]
    public ActionResult<PersonExtDTO> Authetificate([FromBody] LoginRequest request)
    {
        try
        {
            var user = manager.AuthPerson(request.Login, request.Password);

            var userDto = mapper.Map<PersonExtDTO>(user);
            return userDto;
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
