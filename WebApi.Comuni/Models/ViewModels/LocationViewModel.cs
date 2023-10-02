namespace WebApi.Comuni.Models.ViewModels;

public class LocationViewModel
{
    public int ComuneId { get; set; }
    public string Comune { get; set; }
    public string Cap { get; set; }
    public string Provincia { get; set; }
    public string Regione { get; set; }
}