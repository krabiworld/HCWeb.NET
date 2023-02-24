using HCWeb.NET.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCWeb.NET.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationContext _context;

    public IndexModel(ApplicationContext context)
    {
        _context = context;
    }

    public Post[] Posts = Array.Empty<Post>();

    public void OnGet()
    {
        Posts = _context.Posts.OrderByDescending(p => p.UpdatedAt).Take(20).ToArray();
    }
}