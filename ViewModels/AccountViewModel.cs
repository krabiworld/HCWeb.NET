using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Forms;

public class AccountNameViewModel
{
    public string Name { get; set; } = "";
}

public class AccountEmailViewModel
{
    public string Email { get; set; } = "";
}

public class AccountPasswordViewModel
{
    public string CurrentPassword { get; set; } = "";

    public string Password { get; set; } = "";

    [Compare("Password")]
    public string ConfirmPassword { get; set; } = "";
}
