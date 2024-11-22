

using Microsoft.Extensions.DependencyInjection;
using ScreeningExchange.App.Api.Features.Questions.FindQuestionsById;
using ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;

namespace ScreeningExchange.App.Api;

public static class DependencyBuilderModule
{
    public static IServiceCollection AddApiModule(this IServiceCollection services)
        => services
            .AddFindQuestionByIdFeature()
            .AddUpsertQuestionFeature()
            ;
}