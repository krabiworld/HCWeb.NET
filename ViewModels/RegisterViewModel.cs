using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;
}
