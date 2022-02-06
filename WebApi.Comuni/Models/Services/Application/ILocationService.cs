using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Comuni.Models.InputModels;
using WebApi.Comuni.Models.ViewModels;

namespace WebApi.Comuni.Models.Services.Application
{
    public interface ILocationService
    {
        // Task<List<LocationViewModel>> GetLocationsAsync();
        Task<LocationViewModel> GetLocationAsync(ComuneInputModel model);
        Task<List<LocationViewModel>> GetCapAsync(CapInputModel model);
    }
}
