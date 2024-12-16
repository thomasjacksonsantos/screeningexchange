
using Microsoft.Extensions.DependencyInjection;
using ScreeningExchange.Domain.Aggregates.DestinationsAggregate;
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;
using ScreeningExchange.Domain.Aggregates.StudentiesAggregate;
using ScreeningExchange.Infrastructure.DataAccess;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.BuildQuestionsAggregate;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.DestinationAggretation;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.SchoolsAggregate;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.StudentiesAggregate;

namespace ScreeningExchange.Infrastructure.DomainImplementation;

public static class DependencyBuilder
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<EfUnitOfWork<ApplicationDbContext>>();
        services.AddScoped<IUnitOfWork>(factory => factory.GetService<EfUnitOfWork<ApplicationDbContext>>()!);
        services.AddTransient<IBuildQuestionRepository, EfBuildQuestionRepository>();
        services.AddTransient<IStudentRepository, EfStudentRepository>();
        services.AddTransient<IDestinationRepository, EfDestinationRepository>();
        services.AddTransient<ISchoolRepository, EfSchoolRepository>();
        return services;
    }
}