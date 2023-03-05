using HCWeb.NET.Forms;
using HCWeb.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCWeb.NET.Pages;

public class LoginModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [BindProperty]
    public LoginViewModel ViewModel { get; set; } = new LoginViewModel();
    
    public void OnGet() { }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.FindByEmailAsync(ViewModel.Email);

        if (user == null || user.UserName == null)
        {
            ModelState.AddModelError(string.Empty, "User not found.");
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, ViewModel.Password, true, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("ViewModel.Email", "Email or password incorrect.");
            return Page();
        }

        return Redirect("/");
    }
}