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
        var email = User.GetEmail();
        var user = _context.Users.SingleOrDefault(u => u.Email == email);
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
        var email = User.GetEmail();
        var user = _context.Users.SingleOrDefault(u => u.Email == email);
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
        var email = User.GetEmail();
        var user = _context.Users.SingleOrDefault(u => u.Email == email);
        if (user == null)
        {
            ErrorMessage = "User not found";
            return Page();
        }

        user.Email = EmailForm.Email;
        _context.SaveChanges();
        
        return Redirect("/Logout");
    }
    
    public void OnPostPassword()
    {
        
    }
}