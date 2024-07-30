using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Dtos;

public class CreatePostDto
{
    [MinLength(5)]
    public string Title { get; set; } = string.Empty;
    
    [MinLength(100)]
    public string Content { get; set; } = string.Empty;

    public string? Preview { get; set; }
}