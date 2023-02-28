using HCWeb.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCWeb.NET.Pages;

public class PostModel : PageModel
{
    private readonly ApplicationContext _context;

    public PostModel(ApplicationContext context)
    {
        _context = context;
    }
    
    public Post? Post { get; set; }
    
    public async Task<IActionResult> OnGet(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return Redirect("/");
        Post = post;
        return Page();
    }
}