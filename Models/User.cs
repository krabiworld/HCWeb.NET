using System.ComponentModel.DataAnnotations.Schema;

namespace HCWeb.NET.Models;

[Table("users")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; } = "";
    
    [Column("email")]
    public string Email { get; set; } = "";
    
    [Column("password")]
    public string Password { get; set; } = "";
    
    [Column("active")]
    public bool Active { get; set; }
    
    [Column("role_id")]
    public int RoleId { get; set; }
    
    public Role Role { get; set; } = new();
}