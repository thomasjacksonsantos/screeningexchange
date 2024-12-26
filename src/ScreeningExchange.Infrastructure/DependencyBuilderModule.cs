using ScreeningExchange.Infrastructure.Configuration;
using ScreeningExchange.Infrastructure.DataAccess;
using ScreeningExchange.Infrastructure.DomainImplementation;
using ScreeningExchange.Infrastructure.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Azure;

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
        services.ConfigureServiceBus(configuration);
        return services;
    }

    private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiConfig>(configuration.GetSection("AppSettings"));
        services.AddSingleton(c => c.GetService<IOptions<ApiConfig>>()!.Value);

        return services;
    }

    static string GetConnetion(string connection)
    {
        var conn = Environment.GetEnvironmentVariable($"SQLAZURECONNSTR_{connection}");
        if (string.IsNullOrEmpty(conn))
        {
            conn = Environment.GetEnvironmentVariable($"ConnectionStrings_{connection}");
        }

        if (string.IsNullOrEmpty(conn))
        {
            conn = Environment.GetEnvironmentVariable(connection);
        }

        return conn ?? string.Empty;
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

    private static void ConfigureServiceBus(this IServiceCollection service, IConfiguration configuration)
        => service.AddAzureClients(builder => builder.AddServiceBusClient(configuration.GetConnectionString("ServiceBusConnection")));
}