using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Forms;

public class PostViewModel
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    public IFormFile? Preview { get; set; }
}