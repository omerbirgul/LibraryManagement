using Library.Core.Entities;
using Library.Data.Database;
using Microsoft.AspNetCore.Identity;

namespace Library.Api.Extensions;

public static class SecurityExtension
{
    public static void AddSecurityExt(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;

                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();
    }
}