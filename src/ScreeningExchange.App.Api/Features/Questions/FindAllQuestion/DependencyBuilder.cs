using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllQuestion;

public static class DependencyBuilder
{
    public static IServiceCollection AddFindAllQuestionFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindAllQuestionResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindAllQuestionRequest, IUseCaseOutputPort<Result<FindAllQuestionResponse>>, Result<FindAllQuestionResponse>>, UseCase>();
        return services;
    }
}