using Ambulance.BLL.Services;
using AmbulanceSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambulance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DispatcherController : Controller
    {
        //private readonly DispatcherService _dispatcherService;

        //public DispatcherController(DispatcherService dispatcherService)
        //{
        //    _dispatcherService = dispatcherService;
        //}

        // Геокодування адреси хворого
        //[HttpGet("geocode")]
        //public async Task<IActionResult> Geocode([FromQuery] string address)
        //{
        //    try
        //    {
        //        var coords = await _dispatcherService.GeocodeAddressAsync(address);
        //        return Ok(new { Latitude = coords.Lat, Longitude = coords.Lon });
        //    }
        //    catch
        //    {
        //        return BadRequest("Адресу не знайдено");
        //    }
        //}

        // Кращі бригади за типом та ETA
        //[HttpGet("best-brigades")]
        //public async Task<IActionResult> GetBestBrigades(
        //    [FromQuery] int brigadeTypeId,
        //    [FromQuery] double lat,
        //    [FromQuery] double lon,
        //    [FromQuery] int count = 1)
        //{
        //    var brigades = await _dispatcherService.GetBestTeamsByTypeAndEtaAsync(brigadeTypeId, lat, lon, count);
        //    return Ok(brigades);
        //}
    }
}
