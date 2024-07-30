namespace HCWeb.NET.Models;

public class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool EmailConfirmed { get; set; }

    public string Role { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}