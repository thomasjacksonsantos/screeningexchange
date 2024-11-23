using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;

public static class DependencyBuilder
{
    public static IServiceCollection AddFindNextQuestionByAwnserFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindNextQuestionByAwnserResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindNextQuestionByAwnserRequest, IUseCaseOutputPort<Result<FindNextQuestionByAwnserResponse>>, Result<FindNextQuestionByAwnserResponse>>, UseCase>();
        return services;
    }
}