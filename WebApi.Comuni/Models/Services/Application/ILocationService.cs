using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Comuni.Models.ViewModels;

namespace WebApi.Comuni.Models.Services.Application
{
    public interface ILocationService
    {
        // Task<List<LocationViewModel>> GetLocationsAsync();
        Task<LocationViewModel> GetLocationAsync(string location);
        Task<List<LocationViewModel>> GetCapAsync(string cap);
    }
}
