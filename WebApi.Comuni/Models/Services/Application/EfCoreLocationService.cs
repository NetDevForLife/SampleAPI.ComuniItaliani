using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comuni.Models.Extensions;
using WebApi.Comuni.Models.Services.Infrastructure;
using WebApi.Comuni.Models.ViewModels;

namespace WebApi.Comuni.Models.Services.Application
{
    public class EfCoreLocationService : ILocationService
    {
        private readonly ILogger<EfCoreLocationService> logger;
        private readonly ApplicationDbContext dbContext;

        public EfCoreLocationService(ILogger<EfCoreLocationService> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        // public async Task<List<LocationViewModel>> GetLocationsAsync()
        // {
        //     var locations = await dbContext.Locations.ToListAsync();

        //     List<LocationViewModel> lista = new();

        //     lista = locations.Select(location => new LocationViewModel
        //     {
        //         ComuneId = location.ComuneId,
        //         Comune = location.Comune,
        //         Cap = location.Cap,
        //         Provincia = location.Provincia,
        //         Regione = location.Regione
        //     }).ToList();

        //     return lista;
        // }

        public async Task<LocationViewModel> GetLocationAsync(string location)
        {
            IQueryable<LocationViewModel> queryLinq = dbContext.Locations
                .AsNoTracking()
                .Where(comune => comune.Comune == location)
                .Select(comune => comune.ToLocationViewModel());

            LocationViewModel viewModel = await queryLinq.FirstOrDefaultAsync();

            return viewModel;
        }

        public async Task<List<LocationViewModel>> GetCapAsync(string cap)
        {
            var locations = await dbContext.Locations
                .Where(comune => comune.Cap == cap)
                .ToListAsync();

            List<LocationViewModel> lista = new();

            lista = locations.Select(location => new LocationViewModel
            {
                ComuneId = location.ComuneId,
                Comune = location.Comune,
                Cap = location.Cap,
                Provincia = location.Provincia,
                Regione = location.Regione
            }).ToList();

            return lista;
        }
    }
}
