using System.ComponentModel.DataAnnotations;

namespace WebApi.Comuni.Models.InputModels;

public class CapInputModel
{
    [Required]
    public string Cap { get; set; }
}