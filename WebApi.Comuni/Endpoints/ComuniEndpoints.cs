namespace WebApi.Comuni.Endpoints;

public class ComuniEndpoints : IEndpointRouteHandler
{
	public static void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var comuniApiGroup = endpoints
		.MapGroup("/api/comuni");

		comuniApiGroup.MapGet(string.Empty, async Task<Results<Ok<List<LocationViewModel>>, NotFound>> (ILocationService locationService) =>
		{
			var entities = await locationService.GetLocationsAsync();

			if (entities.Count == 0)
			{
				return TypedResults.NotFound();
			}

			return TypedResults.Ok(entities.Select(location => new
			LocationViewModel(location.ComuneId, location.Comune, location.Cap, location.Provincia, location.Regione)).ToList());
		});

		comuniApiGroup.MapGet("comune", async Task<Results<Ok<LocationViewModel>, NotFound, BadRequest>> ([AsParameters] ComuneInputModel request,
			ILocationService locationService) =>
		{
			if (request == null) return TypedResults.BadRequest();

			var entities = await locationService.GetLocationAsync(request);

			if (entities is null)
			{
				return TypedResults.NotFound();
			}

			return TypedResults.Ok(entities);
		});

		comuniApiGroup.MapGet("cap", async Task<Results<Ok<List<LocationViewModel>>, NotFound, BadRequest>> ([AsParameters] CapInputModel request,
			ILocationService locationService) =>
		{
			if (request == null)
			{
				return TypedResults.BadRequest();
			}

			var entities = await locationService.GetCapAsync(request);

			if (entities.Count == 0)
			{
				return TypedResults.NotFound();
			}

			return TypedResults.Ok(entities.Select(location => new
			LocationViewModel(location.ComuneId, location.Comune, location.Cap, location.Provincia, location.Regione)).ToList());
		});
	}
}