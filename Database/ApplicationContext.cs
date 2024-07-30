using HCWeb.NET.Database.Interceptors;
using HCWeb.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace HCWeb.NET.Database;

public class ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        builder.AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Post
        builder.Entity<Post>().Property(p => p.Title).HasMaxLength(100);
        builder.Entity<Post>().Property(p => p.Content).HasMaxLength(100_000);
        builder.Entity<Post>().Property(p => p.Preview).HasMaxLength(200);
        
        builder.Entity<Post>().Property(p => p.Id).HasValueGenerator<GuidValueGenerator>();
        builder.Entity<Post>().Property(p => p.CreatedAt).HasDefaultValueSql("now()");
        builder.Entity<Post>().HasQueryFilter(p => p.DeletedAt == null);

        // User
        builder.Entity<User>().Property(u => u.Username).HasMaxLength(50);
        builder.Entity<User>().Property(u => u.Email).HasMaxLength(50);
        builder.Entity<User>().Property(u => u.Role).HasMaxLength(10);
        builder.Entity<User>().Property(u => u.Password).HasMaxLength(200);
        
        builder.Entity<User>().Property(u => u.Id).HasValueGenerator<GuidValueGenerator>();
        builder.Entity<User>().Property(u => u.Role).HasDefaultValue("user");
        builder.Entity<User>().Property(u => u.EmailConfirmed).HasDefaultValue(false);
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}