using Library.Core.Services;
using Library.Service.Mapping;
using Library.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Service.Extensions;

public static class ServiceExtension
{
    public static void AddServicesExt(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(GeneralMapping));
        
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}