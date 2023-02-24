using System.ComponentModel.DataAnnotations.Schema;

namespace HCWeb.NET.Models;

[Table("roles")]
public class Role
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = "";
}