using System.Security.Claims;
using HCWeb.NET.Forms;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCWeb.NET.Pages;

public class LoginModel : PageModel
{
    private ApplicationContext _context;

    public LoginModel(ApplicationContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public UserForm UserForm { get; set; } = new("", "");

    public string ErrorMessage { get; set; } = "";
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPost()
    {
        if (string.IsNullOrEmpty(UserForm.Email) || string.IsNullOrEmpty(UserForm.Password))
        {
            ErrorMessage = "Email or password is empty.";
            return Page();
        }

        var user = _context.Users.FirstOrDefault(u => u.Email == UserForm.Email);
        if (user == null)
        {
            ErrorMessage = "Email or password incorrect.";
            return Page();
        }

        if (!BCrypt.Net.BCrypt.Verify(UserForm.Password, user.Password))
        {
            ErrorMessage = "Email or password incorrect.";
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
        };

        var claimsIdentify = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentify);

        await HttpContext.SignInAsync(claimsPrincipal);
        return Redirect("/");
    }
}