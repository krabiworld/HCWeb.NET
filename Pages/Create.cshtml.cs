using HCWeb.NET.Extensions;
using HCWeb.NET.Forms;
using HCWeb.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCWeb.NET.Pages;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IHostEnvironment _environment;

    public CreateModel(ApplicationContext context, UserManager<User> userManager, IHostEnvironment environment)
    {
        _context = context;
        _userManager = userManager;
        _environment = environment;
    }
    
    [BindProperty]
    public PostViewModel ViewModel { get; set; } = new();
    
    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found.");
            return Page();
        }

        string? previewFile = null;
        if (ViewModel.Preview != null)
        {
            var preview = Path.Combine(_environment.ContentRootPath, "wwwroot/previews", ViewModel.Preview.FileName);
            await using var fileStream = new FileStream(preview, FileMode.Create);
            await ViewModel.Preview.CopyToAsync(fileStream);
            previewFile = ViewModel.Preview.FileName;
        }
        
        var post = new Post
        {
            Title = ViewModel.Title,
            Content = ViewModel.Content,
            Preview = previewFile,
            User = user
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        
        return Redirect($"/Post/{post.Id}");
    }
}
