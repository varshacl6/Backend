using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Models;

public class MovieRequest
{
    [Required]
    public string Name { get; set; }
}