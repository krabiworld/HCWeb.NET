using HCWeb.NET.Dtos;
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
        if (post == null)
        {
            return NotFound("Post not found");
        }

        return Ok(post);
    }
}