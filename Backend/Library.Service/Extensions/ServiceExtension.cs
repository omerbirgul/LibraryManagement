using Library.Service.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Service.Extensions;

public static class ServiceExtension
{
    public static void AddServicesExt(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(GeneralMapping));
    }
}