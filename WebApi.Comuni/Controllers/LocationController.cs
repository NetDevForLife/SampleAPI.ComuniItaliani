using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Comuni.Models.InputModels;
using WebApi.Comuni.Models.Services.Application;
using WebApi.Comuni.Models.ViewModels;

namespace WebApi.Comuni.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController : Controller
{
    private readonly ILocationService locationService;

    public LocationController(ILocationService locationService)
    {
        this.locationService = locationService;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetLocations()
    // {
    //     var entities = await locationService.GetLocationsAsync();

    //     return Ok(entities.Select(location => new LocationViewModel
    //     {
    //         Comune = location.Comune,
    //         Cap = location.Cap,
    //         Provincia = location.Provincia,
    //         Regione = location.Regione,
    //         ComuneId = location.ComuneId,
    //     }));
    // }

    [HttpGet("FindComune")]
    public async Task<IActionResult> GetLocationFromComune([FromQuery] ComuneInputModel model)
    {
        if (model == null) return BadRequest();

        var entities = await locationService.GetLocationAsync(model);

        return entities switch
        {
            null => NotFound(),
            _ => Ok(entities)
        };
    }

    [HttpGet("FindCap")]
    public async Task<IActionResult> GetLocationFromCap([FromQuery] CapInputModel model)
    {
        if (model == null) return BadRequest();

        var entities = await locationService.GetCapAsync(model);

        if (entities.Count == 0)
        {
            return NotFound();
        }
        else
        {
            return Ok(entities.Select(location => new LocationViewModel
            {
                Comune = location.Comune,
                Cap = location.Cap,
                Provincia = location.Provincia,
                Regione = location.Regione,
                ComuneId = location.ComuneId,
            }));
        }
    }
}