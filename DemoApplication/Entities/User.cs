using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DemoApplication.Entities;

public class User: BaseDateEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Username { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    public string Role { get; set; }
}