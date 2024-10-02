namespace WebApi.Comuni.Models.InputModels;

public class ComuneInputModel
{
	[Required, FromQuery(Name = "comune")]
	public string Comune { get; set; }
}