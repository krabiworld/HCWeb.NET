using System.Security.Claims;
using HCWeb.NET.Extensions;
using HCWeb.NET.Forms;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCWeb.NET.Pages;

[Authorize]
public class AccountModel : PageModel
{
    private readonly ApplicationContext _context;

    public AccountModel(ApplicationContext context)
    {
        _context = context;
    }

    public string ErrorMessage = "";
    
    [BindProperty]
    public AccountNameForm NameForm { get; set; } = new("");
    
    [BindProperty]
    public AccountEmailForm EmailForm { get; set; } = new("");
    
    [BindProperty]
    public AccountPasswordForm PasswordForm { get; set; } = new("", ""); 
    
    public IActionResult OnGet()
    {
        var name = User.FindFirst(ClaimTypes.Name);
        var email = User.FindFirst(ClaimTypes.Email);
        if (name == null || email == null)
        {
            ErrorMessage = "User not found";
            return Page();
        }
        
        NameForm = new AccountNameForm(name.Value);
        EmailForm = new AccountEmailForm(email.Value);
        
        return Page();
    }

    public async Task<IActionResult> OnPostName()
    {
        var user = await User.GetUser(_context);
        if (user == null)
        {
            ErrorMessage = "User not found.";
            return Page();
        }

        user.Name = NameForm.Name;
        await _context.SaveChangesAsync();

        if (User.Identity is not ClaimsIdentity identity)
        {
            ErrorMessage = "User not found.";
            return Page();
        }
        identity.RemoveClaim(User.FindFirst(ClaimTypes.Name));
        identity.AddClaim(new Claim(ClaimTypes.Name, NameForm.Name));

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
        
        return Redirect("/Account");
    }
    
    public async Task<IActionResult> OnPostEmail()
    {
        var user = await User.GetUser(_context);
        if (user == null)
        {
            ErrorMessage = "User not found";
            return Page();
        }

        user.Email = EmailForm.Email;
        await _context.SaveChangesAsync();
        
        return Redirect("/Account");
    }
    
    public async Task<IActionResult> OnPostPassword()
    {
        if (PasswordForm.Password != PasswordForm.RetypePassword)
        {
            ErrorMessage = "Password err";
        }

        var user = await User.GetUser(_context);
        if (user == null)
        {
            ErrorMessage = "User not found";
            return Page();
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(PasswordForm.Password);

        user.Password = hashedPassword;
        await _context.SaveChangesAsync();
        
        return Redirect("/Account");
    }
}