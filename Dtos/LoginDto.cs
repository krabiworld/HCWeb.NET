using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Dtos;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}