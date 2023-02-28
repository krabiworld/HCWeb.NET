using HCWeb.NET.Extensions;
using HCWeb.NET.Forms;
using HCWeb.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCWeb.NET.Pages;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationContext _context;
    private readonly IHostEnvironment _environment;

    public CreateModel(ApplicationContext context, IHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    
    [BindProperty]
    public PostForm PostForm { get; set; } = new("", "", null);
    
    public string ErrorMessage { get; set; } = "";
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(PostForm.Title) || string.IsNullOrEmpty(PostForm.Content))
        {
            ErrorMessage = "Title or content is empty.";
            return Page();
        }

        var user = User.GetUser(_context);
        if (user == null)
        {
            ErrorMessage = "User not found.";
            return Page();
        }

        string? previewFile = null;
        if (PostForm.Preview != null)
        {
            var preview = Path.Combine(_environment.ContentRootPath, "wwwroot/previews", PostForm.Preview.FileName);
            await using var fileStream = new FileStream(preview, FileMode.Create);
            await PostForm.Preview.CopyToAsync(fileStream);
            previewFile = PostForm.Preview.FileName;
        }
        
        var post = new Post
        {
            Title = PostForm.Title,
            Content = PostForm.Content,
            Preview = previewFile,
            User = user
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        
        return Redirect("/");
    }
}