using ScreeningExchange.Infrastructure.Configuration;
using ScreeningExchange.Infrastructure.DataAccess;
using ScreeningExchange.Infrastructure.DomainImplementation;
using ScreeningExchange.Infrastructure.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ScreeningExchange.Infrastructure;

public static class DependencyBuilderModule
{
    public static IServiceCollection AddInfrastructureModule
    (
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionString)
    {
        services.AddDbContext(connectionString);
        services.AddRepositories();
        services.AddConfigurations(configuration);
        services.AddIO();
        services.AddHttpClients(configuration);
        return services;
    }

    private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiConfig>(configuration.GetSection("AppSettings"));
        services.AddSingleton(c => c.GetService<IOptions<ApiConfig>>()!.Value);

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DbContext, ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(configuration.GetSection("AppSettings:FirebaseAuthentication:ServiceName").Value!, c => c.BaseAddress = new Uri(
            configuration.GetSection("AppSettings:FirebaseAuthentication:TokenUri").Value!
        ));

        return services;
    }
}