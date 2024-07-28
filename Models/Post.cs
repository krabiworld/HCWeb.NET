using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Models;

public class Post
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;
    
    public string Content { get; set; } = null!;
    
    public string? Preview { get; set; }

    public string UserId { get; set; } = null!;

    public User User { get; set; } = null!;

    [Timestamp]
    public DateTime UpdatedAt { get; set; }

    [Timestamp]
    public DateTime CreatedAt { get; set; }
}
