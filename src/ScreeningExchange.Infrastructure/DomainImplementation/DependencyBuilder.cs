
using Microsoft.Extensions.DependencyInjection;
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Infrastructure.DataAccess;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.AlertAggregate;

namespace ScreeningExchange.Infrastructure.DomainImplementation;

public static class DependencyBuilder
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<EfUnitOfWork<ApplicationDbContext>>();
        services.AddScoped<IUnitOfWork>(factory => factory.GetService<EfUnitOfWork<ApplicationDbContext>>()!);
        services.AddTransient<IBuildQuestionRepository, EfBuildQuestionRepository>();

        return services;
    }
}