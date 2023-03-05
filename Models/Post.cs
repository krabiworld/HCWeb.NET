using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HCWeb.NET.Models;

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = "";
    
    public string Content { get; set; } = "";
    
    public string? Preview { get; set; }

    public string UserId { get; set; } = null!;

    public User User { get; set; } = new();

    [Timestamp]
    public DateTime UpdatedAt { get; set; }

    [Timestamp]
    public DateTime CreatedAt { get; set; }
}
