using WebApi.Comuni.Models.Entities;
using WebApi.Comuni.Models.ViewModels;

namespace WebApi.Comuni.Models.Extensions;

public static class LocationExtension
{
    public static LocationViewModel ToLocationViewModel(this Location location)
    {
        return new LocationViewModel
        {
            ComuneId = location.ComuneId,
            Comune = location.Comune,
            Cap = location.Cap,
            Provincia = location.Provincia,
            Regione = location.Regione
        };
    }
}