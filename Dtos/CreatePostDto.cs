using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Dtos;

public class CreatePostDto
{
    [MinLength(5)]
    public string Title { get; set; }
    
    [MinLength(100)]
    public string Content { get; set; }

    public string? Preview { get; set; }
}