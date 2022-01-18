using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DemoApplication.Models;

public class RegisterUserRequest
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Username { get; set; }

    public string Password { get; set; }
}