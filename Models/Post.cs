using HCWeb.NET.Database.Interfaces;

namespace HCWeb.NET.Models;

public class Post : ISoftDelete
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;
    
    public string Content { get; set; } = null!;
    
    public string? Preview { get; set; }

    public string UserId { get; set; } = null!;

    public User User { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
