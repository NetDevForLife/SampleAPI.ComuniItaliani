namespace WebApi.Comuni.Models.Entities;

public partial class Location
{
    public int ComuneId { get; set; }
    public string Comune { get; set; }
    public string Cap { get; set; }
    public string Provincia { get; set; }
    public string Regione { get; set; }
}