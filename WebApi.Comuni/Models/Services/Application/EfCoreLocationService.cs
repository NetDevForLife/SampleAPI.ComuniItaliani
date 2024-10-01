namespace WebApi.Comuni.Models.Services.Application;

public class EfCoreLocationService(ApplicationDbContext dbContext) : ILocationService
{
	private readonly ApplicationDbContext dbContext = dbContext;

	public async Task<List<LocationViewModel>> GetLocationsAsync()
	{
		var locations = await dbContext.Locations.ToListAsync();

		return locations.Select(comuni => comuni.ToLocationViewModel()).ToList();
	}

	public async Task<LocationViewModel> GetLocationAsync(ComuneInputModel model)
	{
		IQueryable<LocationViewModel> queryLinq = dbContext.Locations
			.Where(comune => comune.Comune == model.Comune)
			.Select(comune => comune.ToLocationViewModel());

		LocationViewModel viewModel = await queryLinq.FirstOrDefaultAsync();

		return viewModel;
	}

	public async Task<List<LocationViewModel>> GetCapAsync(CapInputModel model)
	{
		var locations = await dbContext.Locations
			.AsNoTracking()
			.Where(comune => comune.Cap == model.Cap)
			.ToListAsync();

		return locations.Select(comuni => comuni.ToLocationViewModel()).ToList();
	}
}