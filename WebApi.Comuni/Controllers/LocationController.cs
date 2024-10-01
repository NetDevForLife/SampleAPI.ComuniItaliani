namespace WebApi.Comuni.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType((int)HttpStatusCode.NotFound)]
public class LocationController(ILocationService locationService) : Controller
{
	private readonly ILocationService locationService = locationService;

	[HttpGet]
	[ProducesResponseType<List<LocationViewModel>>((int)HttpStatusCode.OK)]
	public async Task<Results<Ok<List<LocationViewModel>>, NotFound>> GetLocations()
	{
		var entities = await locationService.GetLocationsAsync();

		if (entities.Count == 0)
		{
			return TypedResults.NotFound();
		}

		return TypedResults.Ok(entities.Select(location => new LocationViewModel(location.ComuneId,
			location.Comune, location.Cap, location.Provincia, location.Regione)).ToList());
	}

	[HttpGet("FindComune")]
	[ProducesResponseType<LocationViewModel>((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IResult> GetLocationFromComune([FromQuery] ComuneInputModel model)
	{
		if (model == null) return TypedResults.BadRequest();

		var entities = await locationService.GetLocationAsync(model);

		if (entities is null)
		{
			return TypedResults.NotFound();
		}

		return TypedResults.Ok(entities);
	}

	[HttpGet("FindCap")]
	[ProducesResponseType<List<LocationViewModel>>((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<Results<Ok<List<LocationViewModel>>, NotFound, BadRequest>> GetLocationFromCap([FromQuery] CapInputModel model)
	{
		if (model == null)
		{
			return TypedResults.BadRequest();
		}

		var entities = await locationService.GetCapAsync(model);

		if (entities.Count == 0)
		{
			return TypedResults.NotFound();
		}

		return TypedResults.Ok(entities.Select(location => new LocationViewModel(location.ComuneId,
			location.Comune, location.Cap, location.Provincia, location.Regione)).ToList());
	}
}