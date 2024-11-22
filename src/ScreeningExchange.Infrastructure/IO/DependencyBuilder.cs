
using ScreeningExchange.Infrastructure.IO.Blobs;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.Infrastructure.IO;

public static class DependencyBuilder
{
    public static IServiceCollection AddIO(this IServiceCollection services)
    {
        services.AddTransient<IStorage, BlobStorage>();
        services.AddTransient<IEmailSend, EmailSend>();
        return services;
    }
}