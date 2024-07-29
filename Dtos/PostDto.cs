using System.ComponentModel.DataAnnotations;
using HCWeb.NET.Models;

namespace HCWeb.NET.Dtos;

public class PostDto(Post post)
{
    public string Id { get; set; } = post.Id;

    public string Title { get; set; } = post.Title;

    public string Content { get; set; } = post.Content;

    public string? Preview { get; set; } = post.Preview;

    public string UserId { get; set; } = post.UserId;

    [Timestamp]
    public DateTimeOffset CreatedAt { get; set; } = post.CreatedAt;
}