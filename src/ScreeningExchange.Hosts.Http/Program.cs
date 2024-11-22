using Microsoft.AspNetCore.Authentication.JwtBearer;
using FastEndpoints;
using NSwag;
using NSwag.Generation.Processors.Security;
using NSwag.AspNetCore;
using ScreeningExchange.App.Api;
using ScreeningExchange.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

services
    // .AddAuthenticationJwtBearer(s => s.SigningKey = "supersecret")
    .AddAuthorization()
    .AddFastEndpoints(o =>
    {
        o.DisableAutoDiscovery = true;
        o.Assemblies = new[] { typeof(AssemblyInfo).Assembly };
    });

services.ConfigureHttpJsonOptions(c =>
{
    c.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    c.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

services
    .AddApiModule()
    .AddInfrastructureModule(
        builder.Configuration,
        builder.Configuration.GetConnectionString("ConnectionsString")!
    );

services.AddOpenApiDocument(configure =>
{
    configure.PostProcess = doc =>
    {
        doc.Info = new OpenApiInfo
        {
            Title = "ScreeningExchange Api",
            Version = "V1",
            Description = "Api Screening Exchange",
            Contact = new OpenApiContact
            {
                Name = "Thomas Jackson",
                Url = "https://www.linkedin.com/in/thomasjacksonsantos/"
            }
        };
    };

    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();
app
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseOpenApi()
    .UseSwaggerUi(settings =>
    {
        settings.OAuth2Client = new OAuth2ClientSettings
        {
            ClientId = builder.Configuration.GetSection("Swagger:AzureB2CClientId").Value,
            AppName = "swagger-ui-client"
        };
        settings.SwaggerRoutes.Add(new SwaggerUiRoute("Doss Api V1", "/swagger/v1/swagger.json"));
        settings.OperationsSorter = "method";
        settings.TagsSorter = "alpha";
    })
    .UseReDoc(options =>
    {
        options.Path = "/redoc";
    });

app.Run();