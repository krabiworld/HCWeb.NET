using System.ComponentModel.DataAnnotations;

namespace HCWeb.NET.Dtos;

public class PostsListDto
{
    [Range(1, 100)] public int Limit { get; set; } = 50;

    public int Offset { get; set; } = 0;
}