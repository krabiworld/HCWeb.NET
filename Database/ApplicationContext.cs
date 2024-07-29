using HCWeb.NET.Database.Interceptors;
using HCWeb.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace HCWeb.NET.Database;

public class ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        builder.AddInterceptors(new UpdatedAtInterceptor(), new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>().Property(p => p.CreatedAt).HasDefaultValueSql("now()");
        builder.Entity<Post>().HasQueryFilter(p => p.DeletedAt == null);

        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}
