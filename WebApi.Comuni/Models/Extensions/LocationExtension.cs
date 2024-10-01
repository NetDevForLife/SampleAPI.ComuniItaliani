namespace WebApi.Comuni.Models.Extensions;

public static class LocationExtension
{
	public static LocationViewModel ToLocationViewModel(this Location location)
	{
		return new LocationViewModel(location.ComuneId, location.Comune, location.Cap, location.Provincia, location.Regione);
	}
}