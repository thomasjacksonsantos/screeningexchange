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
    public enum ApplicationType
    {
        Api,
        AzureFunction
    }

    public static IServiceCollection AddInfrastructureModule
    (
        this IServiceCollection services,
        IConfiguration configuration,
        ApplicationType applicationType
    )
    {
        services.AddDbContext(GetSectionEnvironment("ConnectionsString")!);
        services.AddRepositories();
        services.AddConfigurations(
            configuration,
            applicationType
        );
        services.AddIO();
        services.AddHttpClients(
            configuration,
            applicationType
        );
        services.ConfigureServiceBus(
            configuration,
            applicationType
        );
        return services;
    }

    private static IServiceCollection AddConfigurations(
        this IServiceCollection services,
        IConfiguration configuration,
        ApplicationType applicationType
    )
    {
        switch (applicationType)
        {
            case ApplicationType.Api:
                services.Configure<ApiConfig>(configuration.GetSection("AppSettings"));
                services.AddSingleton(c => c.GetService<IOptions<ApiConfig>>()!.Value);
                break;

            case ApplicationType.AzureFunction:
                services.AddSingleton(c =>
                {
                    var settings = c.GetService<IOptions<ApiConfig>>()!.Value;

                    settings.Email = new Email
                    {
                        DisplayName = Environment.GetEnvironmentVariable("Email_DisplayName")!,
                        From = Environment.GetEnvironmentVariable("Email_From")!,
                        Login = Environment.GetEnvironmentVariable("Email_Login")!,
                        Password = Environment.GetEnvironmentVariable("Email_Password")!,
                        Port = int.Parse(Environment.GetEnvironmentVariable("Email_Port")!),
                        Smtp = Environment.GetEnvironmentVariable("Email_Smtp")!,
                    };

                    settings.WhatsApp = new WhatsApp
                    {
                        AccountSid = Environment.GetEnvironmentVariable("WhatsApp_AccountSid")!,
                        AuthToken = Environment.GetEnvironmentVariable("WhatsApp_AuthToken")!,
                        NumberPhone = Environment.GetEnvironmentVariable("WhatsApp_NumberPhone")!
                    };
                    settings.FirebaseAuthentication = new FirebaseAuthentication
                    {
                        TokenUri = Environment.GetEnvironmentVariable("FirebaseAuthentication_TokenUri")!,
                        ServiceName = Environment.GetEnvironmentVariable("FirebaseAuthentication_ServiceName")!,
                    };

                    return settings;
                });
                break;

            default:
                throw new NotImplementedException("Application type not implementation");
        }

        return services;
    }

    static string GetSectionEnvironment(string connection)
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

    private static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        IConfiguration configuration,
        ApplicationType applicationType
    )
    {
        switch (applicationType)
        {
            case ApplicationType.Api:
                services.AddHttpClient(configuration.GetSection("AppSettings:FirebaseAuthentication:ServiceName").Value!, c => c.BaseAddress = new Uri(
                    configuration.GetSection("AppSettings:FirebaseAuthentication:TokenUri").Value!));
                break;

            case ApplicationType.AzureFunction:
                services.AddHttpClient(Environment.GetEnvironmentVariable("FirebaseAuthentication_ServiceName")!, c => c.BaseAddress = new Uri(
                    Environment.GetEnvironmentVariable("FirebaseAuthentication_TokenUri")!));
                break;

            default:
                throw new NotImplementedException("Application type not implementation");
        }

        return services;
    }

    private static void ConfigureServiceBus(
        this IServiceCollection service,
        IConfiguration configuration,
        ApplicationType applicationType
    )
    {
        switch (applicationType)
        {
            case ApplicationType.Api:
                service.AddAzureClients(builder => builder.AddServiceBusClient(configuration.GetConnectionString("ServiceBusConnection")));
                break;

            case ApplicationType.AzureFunction:
                break;

            default:
                throw new NotImplementedException("Application type not implementation");
        }
    }
}