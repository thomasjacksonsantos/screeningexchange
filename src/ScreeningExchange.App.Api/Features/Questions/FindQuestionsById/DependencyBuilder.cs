using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionsById;

public static class DependencyBuilder
{
    public static IServiceCollection AddFindQuestionByIdFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindQuestionByIdResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindQuestionByIdRequest, IUseCaseOutputPort<Result<FindQuestionByIdResponse>>, Result<FindQuestionByIdResponse>>, UseCase>();
        return services;
    }
}