using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HCWeb.NET.Models;

[Table("posts")]
public class Post
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; } = "";
    
    [Column("content")]
    public string Content { get; set; } = "";
    
    [Column("preview")]
    public string? Preview { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }

    public User User { get; set; } = new();
    
    [Column("updated_at")]
    [Timestamp]
    public DateTime UpdatedAt { get; set; }

    [Column("created_at")]
    [Timestamp]
    public DateTime CreatedAt { get; set; }
}