using HCWeb.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace HCWeb.NET;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}