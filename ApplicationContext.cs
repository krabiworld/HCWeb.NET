using HCWeb.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace HCWeb.NET;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>().Property(p => p.UpdatedAt).HasDefaultValueSql("now()");
        builder.Entity<Post>().Property(p => p.CreatedAt).HasDefaultValueSql("now()");
    }
}
