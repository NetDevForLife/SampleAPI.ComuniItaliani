namespace WebApi.Comuni.Models.InputModels;

public class CapInputModel
{
	[Required, FromQuery(Name = "cap")]
	public string Cap { get; set; }
}