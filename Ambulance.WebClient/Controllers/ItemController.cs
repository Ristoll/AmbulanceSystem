using Ambulance.BLL.Commands;
using Ambulance.BLL.Commands.ItemCommands;
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
public class ItemController : Controller
{
    private readonly ItemCommandManager manager;
    private readonly IMapper mapper;
    public ItemController(ItemCommandManager manager, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(mapper);
        this.manager = manager;
        this.mapper = mapper;
    }

    [HttpPost("create-item")]
    public IActionResult CreateItem([FromBody] ItemDto itemDto)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.CreateItemCommand(itemDto);
            return result ? Ok() : BadRequest("Не вдалося створити виклик");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete-item/{itemId}")]
    public IActionResult DeleteItem(int itemId)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.DeleteItemCommand(itemId);
            return result ? Ok() : BadRequest("Не вдалося видалити виклик");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search-item")]
    public IActionResult SearchItem(int itemId)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.SearchItemCommand(itemId);
            return result ? Ok(result) : BadRequest("Не вдалося знайти користувача");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("load-items")]
    public IActionResult LoadItems()
    {
        try
        {
            var items = manager.LoadItemsCommand();
            return Ok(items);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("assign-item-to-brigade")]
    public IActionResult AssignItemToBrigade([FromBody] BrigadeItemDto brigadeItemDto)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.AssignItemToBrigadeCommand(brigadeItemDto);
            return result ? Ok() : BadRequest("Не вдалося призначити предмет бригаді");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("unassign-item-from-brigade/{brigadeItemId}")]
    public IActionResult UnassignItemFromBrigade(int brigadeItemId)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.UnassignItemFromBrigadeCommand(brigadeItemId);
            return result ? Ok() : BadRequest("Не вдалося відмінити призначення предмета бригаді");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("load-brigade-items")]
    public IActionResult LoadBrigadeItems([FromQuery] int brigadeId)
    {
        try
        {
            var brigadeItems = manager.LoadBrigadeItemsCommand(brigadeId);
            return Ok(brigadeItems);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("increase-brigade-item-quantity")]
    public IActionResult IncreaseBrigadeItemQuantity(int itemId, int amount)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.IncreaseBrigadeItemQuantityCommand(itemId, amount);
            return result ? Ok() : BadRequest("Не вдалося збільшити кількість предмета");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("decrease-brigade-item-quantity")]
    public IActionResult DecreaseBrigadeItemQuantity(int itemId, int amount)
    {
        try
        {
            var actionOwnerId = int.Parse(User.FindFirst("sub")!.Value);
            bool result = manager.DecreaseBrigadeItemQuantityCommand(itemId, amount);
            return result ? Ok() : BadRequest("Не вдалося зменшити кількість предмета");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
