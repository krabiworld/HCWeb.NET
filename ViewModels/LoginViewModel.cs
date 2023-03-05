using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Forms;

public class LoginViewModel
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}