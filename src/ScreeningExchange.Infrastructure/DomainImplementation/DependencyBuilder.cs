
using Microsoft.Extensions.DependencyInjection;
using ScreeningExchange.Domain.Aggregates.AgentsAggregate;
using ScreeningExchange.Domain.Aggregates.DestinationsAggregate;
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;
using ScreeningExchange.Domain.Aggregates.StudentiesAggregate;
using ScreeningExchange.Domain.AuthsAggregate;
using ScreeningExchange.Infrastructure.DataAccess;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.AgentsAggregate;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.AuthsAggregate;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.BuildQuestionsAggregate;
using ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.DestinationsAggretation;
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
        services.AddTransient<IAgentRepository, EfAgentRepository>();
        services.AddTransient<IAuthRepository, FirebaseAuthenticationService>();

        return services;
    }
}