namespace HCWeb.NET.Models;

public class User
{
    public string Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public bool EmailConfirmed { get; set; }
    
    public string Role { get; set; }
    
    public string Password { get; set; }
}
