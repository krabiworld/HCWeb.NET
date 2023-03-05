using HCWeb.NET.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HCWeb.NET;

public class ApplicationContext : IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    
    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>().Property(p => p.UpdatedAt).HasDefaultValueSql("now()");
        builder.Entity<Post>().Property(p => p.CreatedAt).HasDefaultValueSql("now()");
    }
}
