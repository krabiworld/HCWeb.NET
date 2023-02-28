using HCWeb.NET.Extensions;
using HCWeb.NET.Forms;
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
        var user = User.GetUser(_context);
        if (user == null)
        {
            ErrorMessage = "User not found";
            return Page();
        }

        NameForm = new AccountNameForm(user.Name);
        EmailForm = new AccountEmailForm(user.Email);
        
        return Page();
    }

    public IActionResult OnPostName()
    {
        var user = User.GetUser(_context);
        if (user == null)
        {
            ErrorMessage = "User not found";
            return Page();
        }

        user.Name = NameForm.Name;
        _context.SaveChanges();
        
        return Redirect("/Account");
    }
    
    public IActionResult OnPostEmail()
    {
        var user = User.GetUser(_context);
        if (user == null)
        {
            ErrorMessage = "User not found";
            return Page();
        }

        user.Email = EmailForm.Email;
        _context.SaveChanges();
        
        return Redirect("/Account");
    }
    
    public IActionResult OnPostPassword()
    {
        if (PasswordForm.Password != PasswordForm.RetypePassword)
        {
            ErrorMessage = "Password err";
        }

        var user = User.GetUser(_context);
        if (user == null)
        {
            ErrorMessage = "User not found";
            return Page();
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(PasswordForm.Password);

        user.Password = hashedPassword;
        _context.SaveChanges();
        
        return Redirect("/Account");
    }
}