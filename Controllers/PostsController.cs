using System.Security.Claims;
using HCWeb.NET.Dtos;
using HCWeb.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HCWeb.NET.Controllers;

[Route("api/posts")]
[ApiController]
public class PostsController(ApplicationContext context, ILogger<PostsController> logger) : ControllerBase
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
        if (post == null)
        {
            return NotFound("Post not found");
        }

        return Ok(post);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePostDto dto)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier);
        if (id == null)
        {
            logger.LogError("User does not have ClaimTypes.NameIdentifier");
            return BadRequest("Token is invalid");
        }

        var post = new Post
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Content = dto.Content,
            Preview = dto.Preview,
            UserId = id.Value,
        };

        await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();

        return Ok("Post created.");
    }
}