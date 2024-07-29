using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Dtos;

public class UpdatePostDto
{
    [MinLength(5)] public string? Title { get; set; } = null;

    [MinLength(100)] public string? Content { get; set; } = null;

    public string? Preview { get; set; }
}