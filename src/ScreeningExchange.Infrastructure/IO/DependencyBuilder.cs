
using ScreeningExchange.Infrastructure.IO.Blobs;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.Infrastructure.IO;

public static class DependencyBuilder
{
    public static IServiceCollection AddIO(this IServiceCollection services)
    {
        services.AddTransient<IStorage, BlobStorage>();
        services.AddTransient<IEmailSend, EmailSend>();
        services.AddTransient<IExcelRead, ExcelOfficeOpenXml>();
        services.AddTransient<IServiceBus, ServiceBus>();
        services.AddTransient<IWhatsapp, WhatsAppRepository>();
        return services;
    }
}