namespace DemoApplication.Models;

public class LoginUserResponse
{
    public string accessToken { get; set; }
    public int? expiresIn { get; set; }
    public string tokenType { get; set; }
    public int? creationTime { get; set; }
    public int? expirationTime { get; set; }
    public string  username { get; set; }
}