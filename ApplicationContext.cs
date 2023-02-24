using HCWeb.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace HCWeb.NET;

public class ApplicationContext : DbContext
{
    public DbSet<Role> Roles { get; set; } = null!;
    
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(ApplicationConfig.ConnectionString);
}