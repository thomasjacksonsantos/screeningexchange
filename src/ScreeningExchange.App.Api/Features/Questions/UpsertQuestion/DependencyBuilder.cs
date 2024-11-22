using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;

public static class DependencyBuilder
{
    public static IServiceCollection AddUpsertQuestionFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<UpsertQuestionResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<UpsertQuestiondRequest, IUseCaseOutputPort<Result<UpsertQuestionResponse>>, Result<UpsertQuestionResponse>>, UseCase>();
        return services;
    }
}