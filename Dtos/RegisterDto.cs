using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Dtos;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}