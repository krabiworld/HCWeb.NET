using System.ComponentModel.DataAnnotations;
using HCWeb.NET.Models;

namespace HCWeb.NET.Dtos;

public class PostDto
{
    public PostDto(Post post)
    {
        Id = post.Id;
        Title = post.Title;
        Content = post.Content;
        Preview = post.Preview;
        UserId = post.UserId;
        CreatedAt = post.CreatedAt;
    }
    
    public string Id { get; set; }

    public string Title { get; set; }
    
    public string Content { get; set; }

    public string? Preview { get; set; }

    public string UserId { get; set; }

    [Timestamp]
    public DateTime CreatedAt { get; set; }
}