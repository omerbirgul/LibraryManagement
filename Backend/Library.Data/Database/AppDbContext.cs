using System.Reflection;
using Library.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Database;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookRental> BookRentals { get; set; }
    public DbSet<UserRefreshToken> RefreshTokens { get; set; }
}