namespace WebApi.Comuni.Models.Services.Application;

public interface ILocationService
{
	Task<List<LocationViewModel>> GetLocationsAsync();
	Task<LocationViewModel> GetLocationAsync(ComuneInputModel model);
	Task<List<LocationViewModel>> GetCapAsync(CapInputModel model);
}