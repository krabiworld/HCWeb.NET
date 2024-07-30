using HCWeb.NET.Database.Interfaces;

namespace HCWeb.NET.Models;

public class Post : ISoftDelete
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string? Preview { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}