using Library.Core.Repositories;
using Library.Core.UnitOfWork;
using Library.Data.Database;
using Library.Data.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Library.Data.Extensions;

public static class DatabaseExtension
{
    public static void AddDatabaseExt(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringOption = configuration
            .GetSection(ConnectionStringOption.Key)
            .Get<ConnectionStringOption>();
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionStringOption!.DefaultConnection, 
                ServerVersion.AutoDetect(connectionStringOption.DefaultConnection),
                mysqlOptions =>
                {
                    mysqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                });
        });
        
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}