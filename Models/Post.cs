using System.ComponentModel.DataAnnotations;
using HCWeb.NET.Database.Interfaces;

namespace HCWeb.NET.Models;

public class Post : IUpdatedAt, ISoftDelete
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;
    
    public string Content { get; set; } = null!;
    
    public string? Preview { get; set; }

    public string UserId { get; set; } = null!;

    public User User { get; set; } = null!;

    [Timestamp]
    public DateTimeOffset CreatedAt { get; set; }

    [Timestamp]
    public DateTimeOffset? UpdatedAt { get; set; }

    [Timestamp]
    public DateTimeOffset? DeletedAt { get; set; }
}
