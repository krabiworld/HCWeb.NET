using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HCWeb.NET.Database;
using HCWeb.NET.Dtos;
using HCWeb.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HCWeb.NET.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(ApplicationContext context, IConfiguration configuration) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
        };

        var passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, dto.Password);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Send email
        
        return Ok("Check email to confirm registration");
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Password don't match");
        }

        var token = GenerateAccessToken(user.Id, user.Role);

        return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    private JwtSecurityToken GenerateAccessToken(Guid id, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Role, role)
        };

        return new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                SecurityAlgorithms.HmacSha512)
        );
    }
}