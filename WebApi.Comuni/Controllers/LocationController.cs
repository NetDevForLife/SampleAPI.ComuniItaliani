using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Comuni.Models.Services.Application;
using WebApi.Comuni.Models.ViewModels;

namespace WebApi.Comuni.Controllers
{
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

        [HttpGet("FindComune/{comune}")]
        public async Task<IActionResult> GetLocationFromComune(string comune)
        {
            var entities = await locationService.GetLocationAsync(comune);
            if (entities == null)
            {
                return NotFound();
            }

            return Ok(entities);
        }

        [HttpGet("FindCap/{cap}")]
        public async Task<IActionResult> GetLocationFromCap(string cap)
        {
            var entities = await locationService.GetCapAsync(cap);

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
