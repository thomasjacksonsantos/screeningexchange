using Microsoft.AspNetCore.Authentication.JwtBearer;
using FastEndpoints;
using NSwag;
using NSwag.Generation.Processors.Security;
using NSwag.AspNetCore;
using ScreeningExchange.App.Api;
using ScreeningExchange.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using ScreeningExchange.Hosts.Http.Firebase;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddSingleton(
    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Firebase",
                "firebase-config.json"
            )
        ),
    })
);

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(
        JwtBearerDefaults.AuthenticationScheme,
        (o) => { }
    );
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

    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {your token}"
    });

    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

services.AddEndpointsApiExplorer();

services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5174", 
            "https://screening-exchange-bsd3bzg9fkabg6eq.canadacentral-01.azurewebsites.net",
            "https://purple-water-0001e740f.4.azurestaticapps.net/")
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials();
    });
});

WebApplication app = builder.Build();
app
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseOpenApi()
    .UseCors("AllowAll")
    .UseSwaggerUi(settings =>
    {
        // settings.OAuth2Client = new OAuth2ClientSettings
        // {
        //     ClientId = builder.Configuration.GetSection("Swagger:AzureB2CClientId").Value,
        //     AppName = "swagger-ui-client"
        // };
        settings.SwaggerRoutes.Add(new SwaggerUiRoute("Doss Api V1", "/swagger/v1/swagger.json"));
        settings.OperationsSorter = "method";
        settings.TagsSorter = "alpha";
    })
    .UseReDoc(options =>
    {
        options.Path = "/redoc";
    });

app.Run();