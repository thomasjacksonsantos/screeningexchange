using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using ScreeningExchange.Infrastructure;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddInfrastructureModule(
    builder.Configuration,
    DependencyBuilderModule.ApplicationType.AzureFunction
);

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
