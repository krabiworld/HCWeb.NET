using HCWeb.NET.Database;
using HCWeb.NET.Dtos;
using HCWeb.NET.Extensions;
using HCWeb.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HCWeb.NET.Controllers;

[Route("api/posts")]
[ApiController]
public class PostsController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<PostDto[]> List([FromQuery] PostsListDto dto)
    {
        return await context.Posts.OrderByDescending(p => p.CreatedAt).Take(dto.Limit).Skip(dto.Offset)
            .Select(p => new PostDto(p)).ToArrayAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Fetch(string id)
    {
        var post = await context.Posts.Where(p => p.Id == id).Select(p => new PostDto(p)).SingleOrDefaultAsync();
        if (post is null)
        {
            return NotFound("Post not found");
        }

        return Ok(post);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
    {
        var userId = User.Id();

        var post = new Post
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Content = dto.Content,
            Preview = dto.Preview,
            UserId = userId,
        };

        await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();

        return Ok("Post created");
    }

    [HttpPost("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePostDto dto)
    {
        var userId = User.Id();

        var post = await context.Posts.SingleOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (post is null)
        {
            return NotFound("Post not found");
        }

        if (dto.Title != null)
        {
            post.Title = dto.Title;
        }

        if (dto.Content != null)
        {
            post.Content = dto.Content;
        }

        if (dto.Preview != null)
        {
            post.Preview = dto.Preview;
        }

        await context.SaveChangesAsync();

        return Ok("Post updated");
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(string id)
    {
        var userId = User.Id();

        var post = await context.Posts.SingleOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (post is null)
        {
            return NotFound("Post not found");
        }

        context.Posts.Remove(post);
        await context.SaveChangesAsync();

        return Ok("Post deleted");
    }
}