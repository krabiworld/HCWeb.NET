using HCWeb.NET.Forms;
using HCWeb.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCWeb.NET.Pages;

[Authorize]
public class AccountModel : PageModel
{
    private readonly UserManager<User> _userManager;

    public AccountModel(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    [BindProperty]
    public AccountNameViewModel NameViewModel { get; set; } = new();
    
    [BindProperty]
    public AccountEmailViewModel EmailViewModel { get; set; } = new();
    
    [BindProperty]
    public AccountPasswordViewModel PasswordViewModel { get; set; } = new(); 
    
    public async Task<IActionResult> OnGet()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || user.UserName == null || user.Email == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return Page();
        }

        var name = user.UserName;
        var email = user.Email;
        
        NameViewModel.Name = name;
        EmailViewModel.Email = email;
        
        return Page();
    }

    public async Task<IActionResult> OnPostName()
    {
        if (!ModelState.IsValid) return RedirectToAction(null);

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found.");
            return RedirectToAction(null);
        }

        user.UserName = NameViewModel.Name;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                Console.WriteLine(item.Description);
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return RedirectToAction(null);
        }

        return RedirectToAction(null);
    }
    
    public async Task<IActionResult> OnPostEmail()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return RedirectToAction(null);
        }

        user.Email = EmailViewModel.Email;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return RedirectToAction(null);
        }

        return RedirectToAction(null);
    }
    
    public async Task<IActionResult> OnPostPassword()
    {
        if (!ModelState.IsValid) return RedirectToAction(null);

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return RedirectToAction(null);
        }

        var result = await _userManager.ChangePasswordAsync(user, 
            PasswordViewModel.CurrentPassword, PasswordViewModel.Password);

        if (!result.Succeeded)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return RedirectToAction(null);
        }
        
        return RedirectToAction(null);
    }
}
