using System.ComponentModel.DataAnnotations;

namespace WebApi.Comuni.Models.InputModels;

public class ComuneInputModel
{
    [Required]
    public string Comune { get; set; }
}